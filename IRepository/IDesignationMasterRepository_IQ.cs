using WebApplication2.Models;

namespace WebApplication2.IRepository
{
    public interface IDesignationMasterRepository_IQ
    {
        IEnumerable<DesignationMaster> GetAllDesignation();

        DesignationMaster GetDesignationById(int id);
        void AddDesignation(DesignationMaster designation);
        void UpdateDesignation(DesignationMaster designation);

        void DeleteDesignation(int id);

        //List<DesignationMaster> GetAllDesignationUsingDataAdapter();
    }
}
