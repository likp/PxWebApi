    using System;
using System.Collections.Generic;
using System.Text;

using PCAxis.Sql.QueryLib_21;

namespace PCAxis.Sql.Parser_21
{
    public class PXSqlValueSet
    {
        #region 
        
        private string mValueSet;
        //private string mPresText;
        private Dictionary<string, string> mPresText = new Dictionary<string, string>();
        
        private string elimination;
        //private PXSqlValuepool mValuePool;
        private string mValuePoolId;
        private string mValuePres;
        private string mGeoAreaNo;
       
        private string mSortCodeExists;
        
        private int mNumberOfValues;

        /// <summary>
        /// The list of codes(=ids) of Values groups in the order they should appear in selection or presentation. 
        /// The texts++ is found in PxSqlValues
        /// </summary>
        private List<string> sortedListOfCodes = new List<string>();

        public List<string> SortedListOfCodes
        {
            get { return this.sortedListOfCodes; }
            set { this.sortedListOfCodes = value; }
        }
        #endregion


        public string ValueSet
        {
            get { return this.mValueSet; }
           
        }
        
        public Dictionary<string, string> PresText
        {
            get { return this.mPresText; }
            
        }
        

   
        public string Elimination
        {
            get { return this.elimination; }
        }
       
        public string ValuePoolId
        {
            get { return this.mValuePoolId; }
        }

        public string ValuePres
        {
            get { return this.mValuePres; }
           
        }
        public string GeoAreaNo
        {
            get { return this.mGeoAreaNo; }
            
        }

        
        public string SortCodeExists
        {
            get { return mSortCodeExists; }
            
        }

       
        public int NumberOfValues
        {
            get { return mNumberOfValues; }
            set { mNumberOfValues = value; }
        }

        public PXSqlValueSet() { }
        public PXSqlValueSet(QueryLib_21.ValueSetRow inRow) {
            
            this.mValueSet = inRow.ValueSet;
            this.elimination = inRow.Elimination;
            this.mSortCodeExists = inRow.SortCodeExists;
            this.mValuePoolId =  inRow.ValuePool;
            this.mValuePres = inRow.ValuePres;
            this.mGeoAreaNo = inRow.GeoAreaNo;
        


            foreach (string langCode in inRow.texts.Keys) {
               
                //PresText came in version 2.1 and is optional  ...  desciption is up to 200 chars  
                string asPresText = inRow.texts[langCode].PresText;
                if (String.IsNullOrEmpty(asPresText)) {
                    asPresText = inRow.texts[langCode].Description;
                    int gridPosition = asPresText.IndexOf('#');
                    if (gridPosition > 0) {
                        asPresText = asPresText.Substring(0, gridPosition);
                    }
                }
                mPresText[langCode] = asPresText;
               
            }
        //private int mNumberOfValues; is set outside class. Bad thing? Yes
        }

        //for magic all
        public PXSqlValueSet(Dictionary<string, string> presText, string valuePoolId, string elimination, string sortCodeExists,string valuePres)
        {
            this.mValueSet = PCAxis.PlugIn.Sql.PXSqlKeywords.FICTIONAL_ID_ALLVALUESETS;
            this.mPresText = presText;
            this.mValuePoolId = valuePoolId;
            this.elimination = elimination;
            this.mSortCodeExists = sortCodeExists;
            this.mValuePres = valuePres;
            this.mGeoAreaNo = "";
        }
    }
}
