using LeaveManagementSystem1._1.Models.LeaveTypes;

namespace LeaveManagementSystem1._1.Services
{
    public interface ILeaveTypeService
    {
        Task<bool> CheckIfLeaveTypeNameExistsAsync(string name);
        Task<bool> CheckIfLeaveTypeNameExistsAsyncForEdit(LeaveTypesEditVM leaveTypeEdit);
        Task Create(LeaveTypesCreateVM leaveTypeCreateVM);
        Task<T?> Get<T>(int id) where T : class;
        Task<List<LeaveTypeReadOnlyViewModel>> GetAllLeaveTypesAsync();
        Task<bool> LeaveTypeExists(int id);
        Task Remove(int id);
        Task Update(LeaveTypesEditVM leaveTypeEditVM);
    }
}