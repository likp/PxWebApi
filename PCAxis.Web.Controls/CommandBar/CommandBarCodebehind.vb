﻿Imports PCAxis.Web.Core
Imports PCAxis.Paxiom.Localization
Imports PCAxis.Web.Core.Attributes
Imports System.Web.UI.WebControls
Imports System.Collections.ObjectModel
Imports System.Web.UI
Imports PCAxis.Web.Controls.CommandBar.Plugin
Imports PCAxis.Web.Core.Management
Imports PCAxis.Web.Core.Management.LinkManager
Imports PCAxis.Web.Core.Interfaces
Imports System.Web.UI.HtmlControls
Imports System.Collections.Concurrent

Namespace CommandBar
    ''' <summary>
    ''' Host plugins for manipulation and serialization  on the selected <see cref="PCAxis.Paxiom.PXModel" /> and other functions
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CommandBarCodebehind
        Inherits PaxiomControlBase(Of CommandBarCodebehind, CommandBar)
        Private Const EDIT_AND_CALULATE_CAPTION As String = "CtrlCommandBarEditAndCalulateCaption"
        Private Const SAVE_AS_CAPTION As String = "CtrlCommandBarSaveAsCaption"
        Private Const GRAPHS_AND_MAPS_CAPTION As String = "CtrlCommandBarGraphsAndMapsCaption"
        Private Const ACTION_BUTTON_CAPTION As String = "CtrlCommandBarActionButtonCaption"
        Private Const FILE_DOWNLOAD_LINK As String = "CtrlCommandBarFileDownLoadLink"
        Private Const COMMAND_PLUGIN_IMAGES As String = "PluginImages"
        Private Const COMMAND_PLUGIN_SHORTCUT As String = "PluginShortCut"
        Private Const ID_PLUGINCONTROL As String = "PluginControl"
        Private Const FORMAT_IMAGEBUTTON As String = "ImageButton{0}"
        Private Const FORMAT_SHORTCUT_OPERATION As String = "ShortcutOperation{0}"
        Private Const FORMAT_SHORTCUT_LINK As String = "ShortcutLink{0}"
        Private Const FORMAT_SHORTCUT_FILE As String = "ShortcutFile{0}"
        Private Const FORMAT_COMMANDBARSHORTCUT As String = "CommandBarShortcut{0}"
        Private Const ID_PLUGINSELECTOR As String = "PluginSelector"
        Private Const DOWNLOAD_FILE_TITLE As String = "CtrlCommandBarDownloadFileTitle"
        Private Const DOWNLOAD_FILE_INFORMATION As String = "CtrlCommandBarDownloadFileInformation"
        Private Const DOWNLOAD_FILE_LINK As String = "CtrlCommandBarDownloadFileLink"

        Private _isImageButtonsLoaded As Boolean = False
        Private _isPluginIdChanged As Boolean = False        


#Region "Controls"

        Protected CommandBarPanel As Panel
        Protected WithEvents FunctionDropDownList As DropDownList
        Protected WithEvents SaveAsDropDownList As DropDownList
        Protected WithEvents PresentationViewsDropDownList As DropDownList
        Protected FunctionDropDownListShortcuts As Panel
        Protected SaveAsDropDownListShortcuts As Panel
        Protected LinkDropDownListShortcuts As Panel
        Protected CommandBarShortcutsDropDown As Panel
        Protected CommandBarShortcutsImage As Panel
        Protected PluginControlHolder As Panel
        Protected DropDownPanel As Panel
        Protected ButtonPanel As Panel
        Protected ActionButton As Button
        Protected SaveFilePanel As Panel
        Protected SaveFileLink As HyperLink
        Protected commandbarDownloadFileDialog As HtmlGenericControl
        Protected commandbarDownloadFileInformation As Label
        Protected commandbarDownloadFileLink As HyperLink

#End Region

#Region " Properties "
        Private _pluginID As String

        ''' <summary>
        ''' Gets or sets the id of the plugin that is currently loaded
        ''' </summary>
        ''' <value>If a plugin is loaded that is is returned, otherwise the id is <c>null</c></value>
        ''' <returns>The id of the currently loaded plugin or <c>null</c></returns>
        ''' <remarks>This is used to recreate the plugin at postback and to change which plugin to load</remarks>
        <Attributes.PropertyPersistState(Core.Enums.PersistStateType.PerControlAndPage)> _
        Protected Property PluginID() As String
            Get
                Return _pluginID
            End Get
            Set(ByVal value As String)
                _pluginID = value
                If Not Me.IsLoadingState Then
                    _isPluginIdChanged = True
                End If
            End Set
        End Property



        Private _isDropDownsLoaded As Boolean = False
        ''' <summary>
        ''' Gets or sets whether the dropdowns have been loaded
        ''' </summary>
        ''' <value>If <c>True</c> then there is no need to populate the dropdowns, otherwise the dropdowns need to be populated</value>
        ''' <returns><c>True</c> if the dropdowns are loaded, otherwise <c>False</c></returns>
        ''' <remarks></remarks>
        <Attributes.PropertyPersistState(Core.Enums.PersistStateType.PerControlAndPage)> _
        Protected Property IsDropDownsLoaded() As Boolean
            Get
                Return _isDropDownsLoaded
            End Get
            Set(ByVal value As Boolean)
                _isDropDownsLoaded = value
            End Set
        End Property



        ''Private _fileGenerator As FileGenerator
        '''' <summary>
        '''' Gets an instance of <see cref="FileGenerator" /> with the <see cref="CurrentCulture" />
        '''' </summary>
        '''' <value>Instance of <see cref="FileGenerator" /> with the <see cref="CurrentCulture" /></value>
        '''' <returns>An instance of <see cref="FileGenerator" /> with the <see cref="CurrentCulture" /></returns>
        '''' <remarks></remarks>
        'Private ReadOnly Property FileGenerator() As FileGenerator
        '    Get
        '        If _fileGenerator Is Nothing Then
        '            _fileGenerator = New FileGenerator(CurrentCulture)
        '        End If
        '        Return _fileGenerator
        '    End Get
        'End Property

#End Region

#Region " Events"
        ''' <summary>
        ''' Loads clientscripts, recreates the plugin if needed and load the correct view
        ''' </summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">An EventArgs that contains no event data</param>
        ''' <remarks></remarks>
        Private Sub CommandBar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If (QuerystringManager.GetQuerystringParameter("commandbar") = "false") Then
                CommandBarPanel.Visible = False
            ElseIf QuerystringManager.GetQuerystringParameter("downloadfile") IsNot Nothing Then
                DownloadFile(QuerystringManager.GetQuerystringParameter("downloadfile"))
            Else
                LoadScripts()

                'If there is a pluign that has a UI and it needs to be recreated,
                'this will make sure it is loaded so it can handle any events connected to it
                If Not String.IsNullOrEmpty(Me.PluginID) Then
                    HandlePlugin(PluginID)
                End If

                LoadView()
            End If

        End Sub

        ''' <summary>
        ''' Used to display a plugin if one was selected
        ''' </summary>
        ''' <param name="e">An EventArgs that contains no event data</param>
        ''' <remarks></remarks>
        Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
            If (Not _isDropDownsLoaded) And (Not _isImageButtonsLoaded) Then
                LoadView()
            End If

            'If the pluginId has been changed and it's not null or empty
            If _isPluginIdChanged And Not String.IsNullOrEmpty(Me.PluginID) Then
                HandlePlugin(Me.PluginID)
            End If

            MyBase.OnPreRender(e)
        End Sub

        ''' <summary>
        ''' Changes the language of the <see cref="DropDownList"/> and <see cref="ImageButton" /> in the <see cref="CommandBar" />
        ''' </summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">An EventArgs that contains no event data</param>
        ''' <remarks></remarks>
        Private Sub CommandBar_LanguageChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LanguageChanged
            'Create a new filegenerator with the new language
            '_fileGenerator = New FileGenerator(CurrentCulture)

            'Make CommandBar reload itself during PreRender
            _isDropDownsLoaded = False
            _isImageButtonsLoaded = False

        End Sub
#End Region


        ''' <summary>
        ''' Loads the commandbars UI with <see cref="DropDownList" /> or <see cref="ImageButton" /> depending on the <see cref="CommandBar.ViewMode" />
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Sub LoadView()
            ActionButton.Text = Me.GetLocalizedString(ACTION_BUTTON_CAPTION)

            'If _isImageButtonsLoaded Then
            '    UpdateLanguage()
            'End If

            DropDownPanel.Visible = False
            ButtonPanel.Visible = False

            'Ensure there is a CommandBarPlugin filter
            If Marker.CommandBarFilter Is Nothing Then
                Marker.CommandBarFilter = CommandBarFilterFactory.GetFilter(CommandBarPluginFilterType.None.ToString())
            End If

            'Check the viewmode
            Select Case Me.Marker.ViewMode
                Case CommandBarViewMode.DropDown
                    FillDropDowns()
                    DropDownPanel.Visible = True
                Case CommandBarViewMode.ImageButtons
                    IsDropDownsLoaded = False
                    'Only do this if the imagebuttons haven't been created
                    If Not _isImageButtonsLoaded Then
                        FillImagePanel()
                    End If
                    ButtonPanel.Visible = True
            End Select

            'Only do this if the imagebuttons haven't been created
            If Not _isImageButtonsLoaded Then

                'Clear both containers
                CommandBarShortcutsDropDown.Controls.Clear()
                CommandBarShortcutsImage.Controls.Clear()

                Dim plugin As CommandBarPluginInfo
                'Add CommandBar shortcut buttons
                For Each s As String In Marker.CommandbarShortcuts
                    plugin = CommandBarPluginManager.Views(s)
                    If Not plugin Is Nothing Then
                        Dim imageButton As ImageButton = CreatePluginButton(plugin, FORMAT_COMMANDBARSHORTCUT, COMMAND_PLUGIN_SHORTCUT, True, Plugins.Categories.VIEW)
                        Select Case Me.Marker.ViewMode
                            Case CommandBarViewMode.DropDown
                                CommandBarShortcutsDropDown.Controls.Add(imageButton)
                            Case CommandBarViewMode.ImageButtons
                                CommandBarShortcutsImage.Controls.Add(imageButton)
                        End Select
                    End If
                Next

            End If

            Me.SaveFilePanel.Visible = False

            'Set this to ture so that we don't try and create the imagebuttons more than once
            If ButtonPanel.Controls.Count > 0 Then
                'Set this to ture so that we don't try and create the imagebuttons more than once
                _isImageButtonsLoaded = True
            End If

            commandbarDownloadFileDialog.Attributes("title") = Me.GetLocalizedString(DOWNLOAD_FILE_TITLE)
            commandbarDownloadFileInformation.Text = Me.GetLocalizedString(DOWNLOAD_FILE_INFORMATION)
            commandbarDownloadFileLink.Text = Me.GetLocalizedString(DOWNLOAD_FILE_LINK)

        End Sub

        ''' <summary>
        ''' Fills the commandbar with imagebuttons, used when <see cref="CommandBar.ViewMode" /> is <see cref="CommandBarViewMode.ImageButtons" />
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub FillImagePanel()

            ButtonPanel.Controls.Clear()

            'Add Operation buttons
            Dim plugin As CommandBarPluginInfo
            For Each op As String In Marker.OperationButtons
                plugin = CommandBarPluginManager.Operations(op)

                If Not plugin Is Nothing Then
                    If Marker.CommandBarFilter.UsePlugin(plugin, Me.PaxiomModel, Plugins.Categories.OPERATION) Then
                        Dim imageButton As ImageButton = CreatePluginButton(plugin, FORMAT_IMAGEBUTTON, COMMAND_PLUGIN_IMAGES, False, Plugins.Categories.OPERATION)
                        ButtonPanel.Controls.Add(imageButton)
                    End If
                End If
            Next

            'Add filetype buttons
            Dim filetype As FileType
            For Each ft As String In Marker.FiletypeButtons
                If CommandBarPluginManager.FileTypes.ContainsKey(ft) Then
                    'ft is a FileType (for example xls)
                    filetype = CommandBarPluginManager.FileTypes(ft)
                Else
                    'ft is a FileFormat (for example ExcelDoubleColumn)
                    filetype = CommandBarPluginManager.GetFileType(ft)
                End If

                If Not filetype Is Nothing Then
                    If Marker.CommandBarFilter.UseFiletype(filetype) Then
                        Dim imageButton As ImageButton = CreateFiletypeButton(filetype)
                        ButtonPanel.Controls.Add(imageButton)
                    End If
                End If
            Next

            For Each presView As String In Marker.PresentationViewButtons
                plugin = CommandBarPluginManager.Views(presView)

                If Not plugin Is Nothing Then
                    If Marker.CommandBarFilter.UsePlugin(plugin, Me.PaxiomModel, Plugins.Categories.VIEW) Then
                        Dim imageButton As ImageButton = CreatePluginButton(plugin, FORMAT_IMAGEBUTTON, COMMAND_PLUGIN_IMAGES, False, Plugins.Categories.VIEW)
                        ButtonPanel.Controls.Add(imageButton)
                    End If
                End If
            Next

        End Sub


        Private Function IsJavascriptEnabled() As Boolean
            Return System.Web.HttpContext.Current.Request.Browser.EcmaScriptVersion.Major >= 1
        End Function

        ''' <summary>
        ''' Fills the dropdowns in the commandbar with plugins and adds shortcuts under them, used when <see cref="CommandBar.ViewMode" /> is <see cref="CommandBarViewMode.DropDown" />
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub FillDropDowns()
            Dim plugin As CommandBarPluginInfo
            Dim filetype As FileType
            Dim li As ListItem

            'Set DropDowns Enabled/Disabled
            SaveAsDropDownList.Enabled = Marker.CommandBarFilter.DropDownFileFormatsActive
            FunctionDropDownList.Enabled = Marker.CommandBarFilter.DropDownOperationsActive
            PresentationViewsDropDownList.Enabled = Marker.CommandBarFilter.DropDownViewsActive

            'Only do this if the dropdowns haven't already been loaded
            If IsDropDownsLoaded = False Then

                SaveAsDropDownList.Items.Clear()
                FunctionDropDownList.Items.Clear()
                PresentationViewsDropDownList.Items.Clear()

                'Add a heading in every dropdown
                SaveAsDropDownList.Items.Add(Me.GetLocalizedString(SAVE_AS_CAPTION))
                FunctionDropDownList.Items.Add(Me.GetLocalizedString(EDIT_AND_CALULATE_CAPTION))
                'LinkDropDownList.Items.Add(Me.GetLocalizedString(GRAPHS_AND_MAPS_CAPTION))

                'Add plugins to the operations dropdown
                For Each op As String In Marker.Operations
                    plugin = CommandBarPluginManager.Operations(op)

                    If Not plugin Is Nothing Then
                        'Add item to dropdownlist if filter allows it
                        If Marker.CommandBarFilter.UsePlugin(plugin, Me.PaxiomModel, Plugins.Categories.OPERATION) Then
                            FunctionDropDownList.Items.Add(New ListItem(Me.GetLocalizedString(plugin.NameCode), plugin.Name))
                        End If
                    End If
                Next

                'Add fileformats to the "Save as" dropdown
                SaveAsDropDownList.Attributes.Add("onchange", "return downloadSelectedFile(this);")
                For Each outputFormat As String In Marker.OutputFormats
                    'Add item to dropdownlist if filter allows it
                    If Marker.CommandBarFilter.UseOutputFormat(outputFormat) Then
                        Dim downLoadUrl = Request.RawUrl & "?downloadfile=" & outputFormat

                        If Request.QueryString.Count > 0 Then
                            downLoadUrl = Request.RawUrl & "&downloadfile=" & outputFormat
                        End If

                        SaveAsDropDownList.Items.Add(New ListItem(GetLocalizedString(outputFormat), downLoadUrl))
                    End If
                Next

                'Add plugins to the presentation views dropdown
                For Each op As String In Marker.PresentationViews
                    plugin = CommandBarPluginManager.Views(op)

                    If Not plugin Is Nothing Then
                        'Add item to dropdownlist if filter allows it
                        If Marker.CommandBarFilter.UsePlugin(plugin, Me.PaxiomModel, Plugins.Categories.VIEW) Then
                            li = New ListItem(Me.GetLocalizedString(plugin.NameCode), plugin.Name)
                            If li.Value.Equals(Marker.SelectedPresentationView) Then
                                li.Selected = True
                            End If
                            PresentationViewsDropDownList.Items.Add(li)
                        End If

                    End If
                Next

                IsDropDownsLoaded = True
            End If

            'Only do this if the buttons haven't already been loaded
            If Not _isImageButtonsLoaded Then

                FunctionDropDownListShortcuts.Controls.Clear()
                LinkDropDownListShortcuts.Controls.Clear()
                SaveAsDropDownListShortcuts.Controls.Clear()

                'Add shortcut buttons for operations (functions)
                For Each s As String In Marker.OperationShortcuts
                    plugin = CommandBarPluginManager.Operations(s)

                    If Not plugin Is Nothing Then
                        'Add item to dropdownlist if filter allows it
                        If Marker.CommandBarFilter.UsePlugin(plugin, Me.PaxiomModel, Plugins.Categories.OPERATION) Then
                            Dim imageButton As ImageButton = CreatePluginButton(plugin, FORMAT_SHORTCUT_OPERATION, COMMAND_PLUGIN_SHORTCUT, True, Plugins.Categories.OPERATION)
                            FunctionDropDownListShortcuts.Controls.Add(imageButton)
                        End If
                    End If
                Next

                'Add shortcut buttons for file formats
                For Each fileformat As String In Marker.FileformatShortcuts
                    filetype = CommandBarPluginManager.GetFileType(fileformat)

                    If Not filetype Is Nothing Then
                        'Add item to dropdownlist if filter allows it
                        If Marker.CommandBarFilter.UseFiletype(filetype) Then
                            Dim imageButton As ImageButton = CreateFileformatShortcutButton(filetype, fileformat)
                            SaveAsDropDownListShortcuts.Controls.Add(imageButton)
                        End If
                    End If
                Next

                'Add shortcut buttons for presentation views
                For Each s As String In Marker.PresentationViewShortcuts
                    plugin = CommandBarPluginManager.Views(s)

                    If Not plugin Is Nothing Then
                        'Add item to dropdownlist if filter allows it
                        If Marker.CommandBarFilter.UsePlugin(plugin, Me.PaxiomModel, Plugins.Categories.VIEW) Then
                            Dim imageButton As ImageButton = CreatePluginButton(plugin, FORMAT_SHORTCUT_LINK, COMMAND_PLUGIN_SHORTCUT, True, Plugins.Categories.VIEW)
                            LinkDropDownListShortcuts.Controls.Add(imageButton)
                        End If
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' Creates a imagebutton for the given plugin
        ''' </summary>
        ''' <param name="plugin">Plugin to create imagebutton for</param>
        ''' <param name="idString">String used togheter with the plugin name for creating the ID of the imagebutton</param>
        ''' <param name="commandName">CommandName of the imagebutton</param>
        ''' <param name="shortcut">If it is a shortcut button or a normal button that shall be created</param>
        ''' <returns>The created imagebutton</returns>
        ''' <remarks></remarks>
        Private Function CreatePluginButton(ByVal plugin As CommandBarPluginInfo, ByVal idString As String, ByVal commandName As String, ByVal shortcut As Boolean, ByVal pluginCategory As String) As ImageButton
            Dim imageButton As ImageButton = New ImageButton()

            If plugin Is Nothing Then
                Throw New System.ArgumentNullException()
            End If

            With imageButton
                .ID = String.Format(idString, plugin.Name, Globalization.CultureInfo.InvariantCulture)
                .CommandArgument = plugin.Name
                .AlternateText = Me.GetLocalizedString(plugin.NameCode)
                .ToolTip = Me.GetLocalizedString(plugin.NameCode)
                .CssClass = "commandbar_pluginbutton"
                If shortcut Then
                    .ImageUrl = System.IO.Path.Combine(PCAxis.Web.Controls.Configuration.Paths.ImagesPath, plugin.ShortcutImage)
                Else
                    .ImageUrl = System.IO.Path.Combine(PCAxis.Web.Controls.Configuration.Paths.ImagesPath, plugin.Image)
                End If
                .CommandName = commandName
                AddHandler .Command, AddressOf ImageButton_Command

                If Not Marker.CommandBarFilter.UsePlugin(plugin, Me.PaxiomModel, pluginCategory) Then
                    .Visible = False
                End If
            End With

            Return imageButton
        End Function

        Private Function CreateFiletypeButton(ByVal filetype As FileType) As ImageButton
            Dim imageButton As ImageButton = New ImageButton()

            If filetype Is Nothing Then
                Throw New System.ArgumentNullException()
            End If

            With imageButton
                .AlternateText = Me.GetLocalizedString(filetype.TranslatedText)
                .ImageUrl = System.IO.Path.Combine(PCAxis.Web.Controls.Configuration.Paths.ImagesPath, filetype.Image)
                .ToolTip = Me.GetLocalizedString(filetype.TranslatedText)
                .CssClass = "commandbar_pluginbutton"
                .CommandArgument = filetype.Type
                .CommandName = COMMAND_PLUGIN_IMAGES
                .ID = String.Format(FORMAT_IMAGEBUTTON, filetype.Type)
                AddHandler .Command, AddressOf ImageButton_Command
            End With

            Return imageButton
        End Function

        Private Function CreateFileformatShortcutButton(ByVal filetype As FileType, ByVal fileformat As String) As ImageButton
            Dim imageButton As ImageButton = New ImageButton()

            If filetype Is Nothing Then
                Throw New System.ArgumentNullException()
            End If
            If String.IsNullOrEmpty(fileformat) Then
                Throw New System.ArgumentNullException()
            End If

            With imageButton
                .ID = String.Format(FORMAT_SHORTCUT_FILE, fileformat, Globalization.CultureInfo.InvariantCulture)
                .ImageUrl = System.IO.Path.Combine(PCAxis.Web.Controls.Configuration.Paths.ImagesPath, filetype.ShortcutImage)
                .CommandArgument = fileformat
                .AlternateText = Me.GetLocalizedString(fileformat)
                .ToolTip = Me.GetLocalizedString(fileformat)
                .CommandName = COMMAND_PLUGIN_SHORTCUT
                .CssClass = "commandbar_pluginbutton"
                If IsJavascriptEnabled() And (Not String.IsNullOrEmpty(fileformat)) Then
                    Dim downLoadUrl = Request.RawUrl & "?downloadfile=" & fileformat

                    If Request.QueryString.Count > 0 Then
                        downLoadUrl = Request.RawUrl & "&downloadfile=" & fileformat
                    End If
                    '.OnClientClick = "window.open('" + DownloadUrl + "'); return false;"
                    '.OnClientClick = "window.location = " + DownloadUrl + "; return false;"
                    .OnClientClick = "commandbarDownloadFile('" + downLoadUrl + "'); return false;"
                End If
                AddHandler .Command, AddressOf ImageButton_Command
            End With

            Return imageButton
        End Function


        ''' <summary>
        ''' The method is used to add client scripts to the commandbar
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadScripts()
            'Used to hide the action button if javascripts is enabled
            Page.ClientScript.RegisterStartupScript(Me.GetType, "CommandBar_HideActionButton", "PCAxis_HideElement("".commandbar_action"");", True)
        End Sub


        ''' <summary>
        ''' Loads a plugin using the supplied <paramref name="pluginKey" />
        ''' </summary>
        ''' <param name="pluginKey">The unique key of the plugin</param>
        ''' <remarks>Loads plugins with or without UI and fileformats</remarks>
        Private Sub HandlePlugin(ByVal pluginKey As String, Optional ByVal shortcut As Boolean = False)


            'Validate input
            If String.IsNullOrEmpty(pluginKey) Then
                Return
            End If

            Dim c As ConcurrentDictionary(Of String, Plugin.CommandBarPluginInfo)
            Dim pluginCategory As String
            If CommandBarPluginManager.Operations.ContainsKey(pluginKey) Then
                c = CommandBarPluginManager.Operations
                pluginCategory = Plugins.Categories.OPERATION
            Else
                c = CommandBarPluginManager.Views
                pluginCategory = Plugins.Categories.VIEW
            End If

            If c.ContainsKey(pluginKey) Then
                'If the plugins is a commandbar plugin the get it from the plugins collection
                Dim plugin As CommandBarPluginInfo = c(pluginKey)

                If pluginCategory = Plugins.Categories.OPERATION Then
                    HandleOperation(plugin, pluginKey)
                ElseIf pluginCategory = Plugins.Categories.VIEW Then
                    SignalAction(PxActionEventType.Presentation, pluginKey)
                    Marker.ScreenOutputMethod.Invoke(pluginKey, Me.PaxiomModel)
                End If


            ElseIf CommandBarPluginManager.GetFileType(pluginKey) IsNot Nothing Then
                'If the plugin is a fileformat (for example FileTypeExcelDoubleColumn) 
                'load it from filegenerator
                Dim ft As FileType = CommandBarPluginManager.GetFileType(pluginKey)
                LoadFileTypeControl(pluginKey, ft, pluginKey, False)
            Else
                'If the plugin key is a filetype (for example xls)
                Dim fileType As FileType = CommandBarPluginManager.FileTypes(pluginKey)
                Dim showUI As Boolean

                If shortcut Then
                    'Never show UI when shortcut...
                    showUI = False
                Else
                    showUI = True
                End If

                'if the filetype only have one fileformat
                If fileType.FileFormats.Count = 1 Then
                    LoadFileTypeControl(fileType.Type, fileType, fileType.FileFormats.First.Key, showUI)
                Else
                    LoadFileTypeControl(fileType.Type, fileType, String.Empty, showUI)
                End If
            End If

        End Sub

        Private Sub HandleOperation(ByVal plugin As CommandBarPluginInfo, ByVal pluginKey As String)
            'If the control has a UI load it and add it the commandbars plugin container
            If plugin.HasUI Then
                Dim control As ICommandBarGUIPlugin = Nothing
                control = plugin.GetInstance()

                CType(control, Control).ID = ID_PLUGINCONTROL
                AddHandler control.Finished, AddressOf Plugin_Finished

                ShowPlugin(pluginKey, CType(control, Control))
            Else
                'Otherwise execute the plugin immediately and set the result
                SignalAction(PxActionEventType.Operation, pluginKey)
                PaxiomManager.PaxiomModel = plugin.Invoke(Me.PaxiomModel, Page.Controls)
                UpdatePluginVisibility(plugin, Plugins.Categories.OPERATION)
                ClearPlugin()
            End If
        End Sub

        ''' <summary>
        ''' If the plugin have constraints the operation may have made that the plugin shall no longer be visible to the user.
        ''' This method checks the constraints of the plugin and updates it´s visibility
        ''' </summary>
        ''' <param name="plugin">The plugin to update visibility for</param>
        ''' <remarks></remarks>
        Private Sub UpdatePluginVisibility(ByVal plugin As CommandBarPluginInfo, ByVal pluginCategory As String)
            If plugin.Constraints.Count > 0 Then
                'If Not CheckPropertyValues(plugin.Constraints()) Then
                If Not Marker.CommandBarFilter.UsePlugin(plugin, Me.PaxiomModel, pluginCategory) Then
                    'The plugins do not fullfill constraints anymore - Hide the plugin!

                    If Marker.ViewMode = CommandBarViewMode.ImageButtons Then
                        'Hide image button
                        HidePluginImageButton(String.Format(FORMAT_IMAGEBUTTON, plugin.Name))
                    ElseIf Marker.ViewMode = CommandBarViewMode.DropDown Then
                        'Remove plugin from operations dropdown
                        HidePluginDropDown(FunctionDropDownList, plugin.Name)

                        'Remove plugin from links dropdown
                        HidePluginDropDown(PresentationViewsDropDownList, plugin.Name)

                        'Hide operation shortcut button
                        HidePluginImageButton(String.Format(FORMAT_SHORTCUT_OPERATION, plugin.Name))

                        'Hide links shortcut button
                        HidePluginImageButton(String.Format(FORMAT_SHORTCUT_LINK, plugin.Name))
                    End If

                    'Hide CommandBar shortcut button
                    HidePluginImageButton(String.Format(FORMAT_COMMANDBARSHORTCUT, plugin.Name))
                End If
            End If
        End Sub

        ''' <summary>
        ''' Hides a imagebutton
        ''' </summary>
        ''' <param name="id">Id of the image button</param>
        ''' <remarks></remarks>
        Private Sub HidePluginImageButton(ByVal id As String)
            Dim btn As ImageButton

            btn = CType(Me.FindControl(id), ImageButton)
            If Not btn Is Nothing Then
                btn.Visible = False
            End If
        End Sub

        ''' <summary>
        ''' Removes a plugin from a dropdown list
        ''' </summary>
        ''' <param name="dropdown">The dropdown list to remove plugin from</param>
        ''' <param name="pluginId">Id of the plugin to remove</param>
        ''' <remarks></remarks>
        Private Sub HidePluginDropDown(ByVal dropdown As DropDownList, ByVal pluginId As String)
            For Each li As ListItem In dropdown.Items
                If li.Value.Equals(pluginId) Then
                    dropdown.Items.Remove(li)
                    Exit Sub
                End If
            Next
        End Sub

        ''' <summary>
        ''' Loads a filetypes webcontrol
        ''' </summary>
        ''' <param name="pluginKey">The key to use to make sure the control gets displayed again</param>
        ''' <param name="ft">The <see cref="FileType" /> of the control</param>
        ''' <param name="fileFormat">The fileformat to use. Can be empty</param>
        ''' <remarks></remarks>
        Private Sub LoadFileTypeControl(ByVal pluginKey As String, ByVal ft As FileType, ByVal fileFormat As String, ByVal showUI As Boolean)
            'Load the filetypes webcontrol
            Dim fileTypeControl As IFileTypeControl = CType(Activator.CreateInstance(Type.GetType(ft.WebControl)), IFileTypeControl)

            With fileTypeControl
                CType(fileTypeControl, Control).ID = pluginKey
                .SelectedFormat = If(fileFormat Is Nothing, String.Empty, fileFormat)
                .SelectedFileType = ft
                .ShowUI = showUI
            End With

            AddHandler fileTypeControl.Finished, AddressOf FileType_Finished

            'Show the filetypes webcontrol
            ShowPlugin(pluginKey, CType(fileTypeControl, Control))
        End Sub
        ''' <summary>
        ''' Makes sure the supplied <paramref name="pluginControl" /> is displayed
        ''' </summary>
        ''' <param name="pluginKey">The key to use to make sure the control gets displayed again</param>
        ''' <param name="pluginControl">The control to display</param>
        ''' <remarks></remarks>
        Private Sub ShowPlugin(ByVal pluginKey As String, ByVal pluginControl As Control)
            Me._pluginID = pluginKey

            With PluginControlHolder
                .Visible = True
                .Controls.Clear()
                .Controls.Add(pluginControl)
            End With
        End Sub

        ''' <summary>
        ''' Operation selected
        ''' </summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">An EventArgs that contains no event data</param>
        ''' <remarks></remarks>
        Private Sub DropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FunctionDropDownList.SelectedIndexChanged

            Dim dropdown As DropDownList = TryCast(sender, DropDownList)

            If dropdown IsNot Nothing Then
                If dropdown.SelectedIndex > 0 Then
                    'Doesn't load the plugin directly but sets the PluginId instead 
                    'This is done so that when we download a file selected in dropdown and the dropdown isn't reset
                    'and we press a shortcut later the dropdown doesn't override the buttons plugin
                    Me.PluginID = dropdown.SelectedItem.Value
                    dropdown.SelectedIndex = 0
                End If
            End If

        End Sub

        ''' <summary>
        ''' Presentation view selected
        ''' </summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">An EventArgs that contains no event data</param>
        ''' <remarks></remarks>
        Private Sub PresentationViewsDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PresentationViewsDropDownList.SelectedIndexChanged

            Dim dropdown As DropDownList = TryCast(sender, DropDownList)

            If dropdown IsNot Nothing Then
                'Doesn't load the plugin directly but sets the PluginId instead 
                'This is done so that when we download a file selected in dropdown and the dropdown isn't reset
                'and we press a shortcut later the dropdown doesn't override the buttons plugin
                Me.PluginID = dropdown.SelectedItem.Value
                dropdown.SelectedIndex = 0
            End If

        End Sub

        ''' <summary>
        ''' Used by the file download <see cref="DropDownList" /> in the commandbar
        ''' </summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">An EventArgs that contains no event data</param>
        ''' <remarks></remarks>
        Private Sub SaveAsDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveAsDropDownList.SelectedIndexChanged
            Dim selectedValue As String

            selectedValue = SaveAsDropDownList.SelectedValue
            SaveAsDropDownList.SelectedIndex = 0
            DownloadFile(System.Web.HttpUtility.ParseQueryString(selectedValue)("downloadfile"))
        End Sub

        ''' <summary>
        ''' 1. Show download link for the selected file format.
        ''' 2. Reset the commandbar
        ''' 3. If javascript is enabled download is done automatically
        ''' </summary>
        ''' <param name="pluginID">File format</param>
        ''' <remarks></remarks>
        Private Sub CreateDownloadLink(ByVal pluginID As String)
            If String.IsNullOrEmpty(pluginID) Then
                'Cancel button clicked
                ClearPlugin()
                Exit Sub
            Else
                Dim downLoadUrl = Request.RawUrl & "?downloadfile=" & pluginID

                If Request.QueryString.Count > 0 Then
                    downLoadUrl = Request.RawUrl & "&downloadfile=" & pluginID
                End If

                Me.SaveFileLink.NavigateUrl = downLoadUrl
                Me.SaveFilePanel.Visible = True
                'Set localized text on the download link
                SaveFileLink.Text = GetLocalizedString(FILE_DOWNLOAD_LINK)
                ClearPlugin()
                'Call script that automatically starts the download
                Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "download", "jQuery(document).ready(function(){automaticFileDownload('" & SaveFileLink.ClientID & "')});", True)
            End If
        End Sub

        ''' <summary>
        ''' Downloads a file in the selected file format
        ''' </summary>
        ''' <param name="pluginID">File format</param>
        ''' <remarks></remarks>
        Private Sub DownloadFile(ByVal pluginID As String)
            If CommandBarPluginManager.GetFileType(pluginID) IsNot Nothing Then
                Dim ft As FileType = CommandBarPluginManager.GetFileType(pluginID)
                Dim fileTypeControl As IFileTypeControl = CType(Activator.CreateInstance(Type.GetType(ft.WebControl)), IFileTypeControl)

                With fileTypeControl
                    CType(fileTypeControl, Control).ID = pluginID
                    .SelectedFormat = pluginID
                    .SelectedFileType = ft
                    .ShowUI = False
                End With

                SignalAction(PxActionEventType.SaveAs, pluginID)

                fileTypeControl.SerializeAndStream()
            End If
        End Sub

        ''' <summary>
        ''' Called by all <see cref="ImageButton" /> in the commandbar to make the clicked <see cref="ImageButton" /> plugin to be loaded
        ''' </summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">A CommandEventArgs that contains the event data</param>
        ''' <remarks>If the <see cref="ImageButton" /> has more than one plugin connected to it, a <see cref="CommandBarPluginSelector"/> is shown</remarks>
        Private Sub ImageButton_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)

            'If this is a plugin
            If e.CommandName = COMMAND_PLUGIN_IMAGES Then
                Me.PluginID = e.CommandArgument.ToString

            ElseIf e.CommandName = COMMAND_PLUGIN_SHORTCUT Then
                'if this is shortcut, use the commandargument as a plugin key and HandlePlugin will get the correct pluign to load
                'Shortcuts can only have one plugin
                Me.PluginID = e.CommandArgument.ToString
            End If

        End Sub

        ''' <summary>
        ''' Called when a file format has been selected for the active file type user control
        ''' </summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">An EventArgs that contains no event data</param>
        ''' <remarks></remarks>
        Private Sub FileType_Finished(ByVal sender As Object, ByVal e As EventArgs)
            'CreateDownloadLink(CType(Me.PluginControlHolder.Controls(0), IFileTypeControl).SelectedFormat)

            SignalAction(PxActionEventType.SaveAs, PluginID)

            ClearPlugin()
        End Sub

        ''' <summary>
        ''' Called when a plugin with a UI is finished
        ''' </summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">An <see cref="CommandBarPluginFinishedEventArgs" /> that contains the event data</param>
        ''' <remarks></remarks>
        Private Sub Plugin_Finished(ByVal sender As Object, ByVal e As CommandBarPluginFinishedEventArgs)

            If e.PaxiomModel IsNot Nothing Then
                PaxiomManager.PaxiomModel = e.PaxiomModel
                SignalAction(PxActionEventType.Operation, PluginID)
            End If
            ClearPlugin()
        End Sub

        ''' <summary>
        ''' Called when the <see cref="CommandBarPluginSelector" /> has selected a plugin
        ''' </summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">An <see cref="CommandBarPluginSelectorPluginSelectedEventArgs" /> that contains the event data</param>
        ''' <remarks></remarks>
        Private Sub PluginSelector_PluginSelected(ByVal sender As Object, ByVal e As CommandBarPluginSelectorPluginSelectedEventArgs)
            HandlePlugin(e.SelectedPluginName)
        End Sub

        ''' <summary>
        ''' Removes the active plugin from being displayed
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ClearPlugin()
            'Clear the id so the control doesn't get recreate on postback
            Me.PluginID = Nothing

            'Clear the pluginholder
            With Me.PluginControlHolder
                .Controls.Clear()
                .Visible = False
            End With
        End Sub


        ''' <summary>
        ''' Signal PX action to listeners
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SignalAction(ByVal actionType As PxActionEventType, ByVal actionName As String)
            Dim args As New PxActionEventArgs(actionType, actionName)
            PxActionEventHelper.SetModelProperties(args, PaxiomManager.PaxiomModel)
            Marker.OnPxActionEvent(args)
        End Sub
    End Class

End Namespace
