using SportsStoreManagementSystem.Entities;
using SportsStoreManagementSystem.DAL;

namespace SportsStoreManagementSystem.BL
{
    public class ProcurementDetailsBL
    {
        readonly ProcurementDetailsDAL procureObj = new ProcurementDetailsDAL();
        
        public IEnumerable<ProcurementDetail> GetAllProcurementDetailsBL()
        {
            return procureObj.GetAllProcurementDetailsDAL();
        }

        public bool AddProcurementDetailBL(ProcurementDetail procurementDetail)
        {
            return procureObj.AddProcurementDetailDAL(procurementDetail);
        }
    }
}
