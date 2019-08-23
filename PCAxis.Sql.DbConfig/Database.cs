﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Koden er generert av et verktøy.
//     Kjøretidsversjon:2.0.50727.3634
//
//     Endringer i denne filen kan føre til feil virkemåte, og vil gå tapt hvis
//     koden genereres på nytt.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 
namespace PCAxis.Sql.DbConfig {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class Database {
        
        private DatabaseDescription[] descriptionsField;
        
        private DatabaseConnection connectionField;
        
        private DatabaseLogInfo logInfoField;
        
        private LanguageType[] languagesField;
        
        private CodeType[] codesField;
        
        private KeywordType[] keywordsField;
        
        private TableType[] tablesField;
        
        private string idField;
        
        private string metaModelField;
        
        private bool allowConfigDefaultsField;
        
        public Database() {
            this.allowConfigDefaultsField = true;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Description", IsNullable=false)]
        public DatabaseDescription[] Descriptions {
            get {
                return this.descriptionsField;
            }
            set {
                this.descriptionsField = value;
            }
        }
        
        /// <remarks/>
        public DatabaseConnection Connection {
            get {
                return this.connectionField;
            }
            set {
                this.connectionField = value;
            }
        }
        
        /// <remarks/>
        public DatabaseLogInfo LogInfo {
            get {
                return this.logInfoField;
            }
            set {
                this.logInfoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Language", IsNullable=false)]
        public LanguageType[] Languages {
            get {
                return this.languagesField;
            }
            set {
                this.languagesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Code", IsNullable=false)]
        public CodeType[] Codes {
            get {
                return this.codesField;
            }
            set {
                this.codesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Keyword", IsNullable=false)]
        public KeywordType[] Keywords {
            get {
                return this.keywordsField;
            }
            set {
                this.keywordsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Table", IsNullable=false)]
        public TableType[] Tables {
            get {
                return this.tablesField;
            }
            set {
                this.tablesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string metaModel {
            get {
                return this.metaModelField;
            }
            set {
                this.metaModelField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool allowConfigDefaults {
            get {
                return this.allowConfigDefaultsField;
            }
            set {
                this.allowConfigDefaultsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class DatabaseDescription {
        
        private string langField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string lang {
            get {
                return this.langField;
            }
            set {
                this.langField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TableType {
        
        private ColumnType[] columnsField;
        
        private string modelNameField;
        
        private string tableNameField;
        
        private string aliasField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Column", IsNullable=false)]
        public ColumnType[] Columns {
            get {
                return this.columnsField;
            }
            set {
                this.columnsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string modelName {
            get {
                return this.modelNameField;
            }
            set {
                this.modelNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string tableName {
            get {
                return this.tableNameField;
            }
            set {
                this.tableNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string alias {
            get {
                return this.aliasField;
            }
            set {
                this.aliasField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ColumnType {
        
        private string modelNameField;
        
        private string columnNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string modelName {
            get {
                return this.modelNameField;
            }
            set {
                this.modelNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string columnName {
            get {
                return this.columnNameField;
            }
            set {
                this.columnNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LanguageType {
        
        private bool mainField;
        
        private string nameField;
        
        private string codeField;
        
        private string metaSuffixField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool main {
            get {
                return this.mainField;
            }
            set {
                this.mainField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string metaSuffix {
            get {
                return this.metaSuffixField;
            }
            set {
                this.metaSuffixField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LogConnectionType {
        
        private string connectionStringField;
        
        private string logUserField;
        
        private string logPasswordField;
        
        private string[] textField;
        
        private LogConnectionTypeDataProvider dataProviderField;
        
        private LogConnectionTypeDatabaseType databaseTypeField;
        
        private string logtableSchemaField;
        
        /// <remarks/>
        public string ConnectionString {
            get {
                return this.connectionStringField;
            }
            set {
                this.connectionStringField = value;
            }
        }
        
        /// <remarks/>
        public string LogUser {
            get {
                return this.logUserField;
            }
            set {
                this.logUserField = value;
            }
        }
        
        /// <remarks/>
        public string LogPassword {
            get {
                return this.logPasswordField;
            }
            set {
                this.logPasswordField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public LogConnectionTypeDataProvider dataProvider {
            get {
                return this.dataProviderField;
            }
            set {
                this.dataProviderField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public LogConnectionTypeDatabaseType databaseType {
            get {
                return this.databaseTypeField;
            }
            set {
                this.databaseTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string logtableSchema {
            get {
                return this.logtableSchemaField;
            }
            set {
                this.logtableSchemaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum LogConnectionTypeDataProvider {
        
        /// <remarks/>
        OleDb,
        
        /// <remarks/>
        Oracle,
        
        /// <remarks/>
        Sql,
        
        /// <remarks/>
        Odbc,
        
        /// <remarks/>
        SqlCe,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum LogConnectionTypeDatabaseType {
        
        /// <remarks/>
        MSSQLSERVER,
        
        /// <remarks/>
        ORACLE,
        
        /// <remarks/>
        SYBASE,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("")]
        Item,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class KeywordType {
        
        private string modelNameField;
        
        private string keywordNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string modelName {
            get {
                return this.modelNameField;
            }
            set {
                this.modelNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string keywordName {
            get {
                return this.keywordNameField;
            }
            set {
                this.keywordNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CodeType {
        
        private string codeNameField;
        
        private string codeValueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codeName {
            get {
                return this.codeNameField;
            }
            set {
                this.codeNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codeValue {
            get {
                return this.codeValueField;
            }
            set {
                this.codeValueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConnectionType {
        
        private string connectionStringField;
        
        private string defaultUserField;
        
        private string defaultPasswordField;
        
        private string keyForUserField;
        
        private string keyForPasswordField;
        
        private string[] textField;
        
        private ConnectionTypeDataProvider dataProviderField;
        
        private ConnectionTypeDatabaseType databaseTypeField;
        
        private bool useTemporaryTablesField;
        
        private string metatablesSchemaField;
        
        public ConnectionType() {
            this.keyForUserField = "User Id";
            this.keyForPasswordField = "Password";
        }
        
        /// <remarks/>
        public string ConnectionString {
            get {
                return this.connectionStringField;
            }
            set {
                this.connectionStringField = value;
            }
        }

        public string ConnectionStringProvider { get; set; }
        
        /// <remarks/>
        public string DefaultUser {
            get {
                return this.defaultUserField;
            }
            set {
                this.defaultUserField = value;
            }
        }
        
        /// <remarks/>
        public string DefaultPassword {
            get {
                return this.defaultPasswordField;
            }
            set {
                this.defaultPasswordField = value;
            }
        }
        
        /// <remarks/>
        [System.ComponentModel.DefaultValueAttribute("User Id")]
        public string KeyForUser {
            get {
                return this.keyForUserField;
            }
            set {
                this.keyForUserField = value;
            }
        }
        
        /// <remarks/>
        [System.ComponentModel.DefaultValueAttribute("Password")]
        public string KeyForPassword {
            get {
                return this.keyForPasswordField;
            }
            set {
                this.keyForPasswordField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ConnectionTypeDataProvider dataProvider {
            get {
                return this.dataProviderField;
            }
            set {
                this.dataProviderField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ConnectionTypeDatabaseType databaseType {
            get {
                return this.databaseTypeField;
            }
            set {
                this.databaseTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool useTemporaryTables {
            get {
                return this.useTemporaryTablesField;
            }
            set {
                this.useTemporaryTablesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string metatablesSchema {
            get {
                return this.metatablesSchemaField;
            }
            set {
                this.metatablesSchemaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ConnectionTypeDataProvider {
        
        /// <remarks/>
        OleDb,
        
        /// <remarks/>
        Oracle,
        
        /// <remarks/>
        Sql,
        
        /// <remarks/>
        Odbc,
        
        /// <remarks/>
        SqlCe,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ConnectionTypeDatabaseType {
        
        /// <remarks/>
        MSSQLSERVER,
        
        /// <remarks/>
        ORACLE,
        
        /// <remarks/>
        SYBASE,
        
        /// <remarks/>
        MYSQL,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class DatabaseConnection : ConnectionType {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class DatabaseLogInfo {
        
        private bool tracelogField;
        
        private DatabaseLogInfoUsagelog usagelogField;
        
        /// <remarks/>
        public bool Tracelog {
            get {
                return this.tracelogField;
            }
            set {
                this.tracelogField = value;
            }
        }
        
        /// <remarks/>
        public DatabaseLogInfoUsagelog Usagelog {
            get {
                return this.usagelogField;
            }
            set {
                this.usagelogField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class DatabaseLogInfoUsagelog {
        
        private bool doLogUsageField;
        
        private object logTableNameField;
        
        private LogConnectionType connectionField;
        
        /// <remarks/>
        public bool DoLogUsage {
            get {
                return this.doLogUsageField;
            }
            set {
                this.doLogUsageField = value;
            }
        }
        
        /// <remarks/>
        public object LogTableName {
            get {
                return this.logTableNameField;
            }
            set {
                this.logTableNameField = value;
            }
        }
        
        /// <remarks/>
        public LogConnectionType Connection {
            get {
                return this.connectionField;
            }
            set {
                this.connectionField = value;
            }
        }
    }
}
