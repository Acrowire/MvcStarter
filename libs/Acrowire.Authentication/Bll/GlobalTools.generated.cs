namespace Acrowire.Bll {
    
    
    internal partial class GlobalTools {
        
        internal static bool IsSafeDataSet(System.Data.DataSet ds) {
            return GlobalTools.IsSafeDataSet(ds, 1);
        }
        
        internal static bool IsSafeDataSet(System.Data.DataSet ds, int nbTables) {
            if ((((ds != null) 
                        && (ds.Tables != null)) 
                        && (ds.Tables.Count == nbTables))) {
                for (int i = 0; (i < nbTables); i = (i + 1)) {
                    if ((ds.Tables[i].Rows == null)) {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
