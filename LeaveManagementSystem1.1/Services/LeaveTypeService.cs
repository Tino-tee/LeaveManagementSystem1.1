using AutoMapper;
using LeaveManagementSystem1._1.Data;
using LeaveManagementSystem1._1.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LeaveManagementSystem1._1.Services
{
    public class _LeaveTypeService(ApplicationDbContext context, IMapper mapper) : ILeaveTypeService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<LeaveTypeReadOnlyViewModel>> GetAllLeaveTypesAsync()
        {
            var data = await _context.LeaveTypes.ToListAsync();
            var viewData = _mapper.Map<List<LeaveTypeReadOnlyViewModel>>(data);
            return viewData;
        }

        // Make the method generic by adding <T>
        public async Task<T?> Get<T>(int id) where T : class
        {
            var data = await _context.LeaveTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (data == null)
            {
                return null;
            }
            var viewData = _mapper.Map<T>(data);
            return viewData;
        }
        public async Task Remove(int id)
        {
            var data = await _context.LeaveTypes.FindAsync(id);
            if (data != null)
            {
                _context.LeaveTypes.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Create(LeaveTypesCreateVM leaveTypeCreateVM)
        {
            var data = _mapper.Map<LeaveType>(leaveTypeCreateVM);
            _context.LeaveTypes.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task Update(LeaveTypesEditVM leaveTypeEditVM)
        {
            var data = _mapper.Map<LeaveType>(leaveTypeEditVM);
            _context.LeaveTypes.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfLeaveTypeNameExistsAsync(string name)
        {
            return await _context.LeaveTypes.AnyAsync(l => l.Name == name);
        }

        public async Task<bool> CheckIfLeaveTypeNameExistsAsyncForEdit(LeaveTypesEditVM leaveTypeEdit)
        {
            // return true if any other leave type has the same name
            return await _context.LeaveTypes
                .AnyAsync(l => l.Name == leaveTypeEdit.Name && l.Id != leaveTypeEdit.Id);
        }

        public async Task<bool> LeaveTypeExists(int id)
        {
            return await _context.LeaveTypes.AnyAsync(l => l.Id == id);
        }
    }
}

