using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem1._1.Data;
using LeaveManagementSystem1._1.Models.LeaveTypes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using LeaveManagementSystem1._1.Services;

public class LeaveTypesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeService _leaveTypeService;
    private const string NameExistsValidationMessage = "A leave type with this name already exists in the system.";

    public LeaveTypesController(ApplicationDbContext context, IMapper mapper, ILeaveTypeService leaveTypeService)
    {
        _context = context;
        _mapper = mapper;
        _leaveTypeService = leaveTypeService;
    }

    // GET: LEAVETYPES
    public async Task<IActionResult> Index()    
    {
        var data = await _context.LeaveTypes.ToListAsync();
        var viewData = _mapper.Map<List<LeaveTypeReadOnlyViewModel>>(data);

        
       

        return View(viewData);
    }

    // GET: LEAVETYPES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _leaveTypeService.Get<LeaveType>(id.Value);
            
        if (leavetype == null)
        {
            return NotFound();
        }
        var viewData = _mapper.Map<LeaveTypeReadOnlyViewModel>(leavetype);
        return View(viewData);
    }

    // GET: LEAVETYPES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LEAVETYPES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveTypesCreateVM LeaveTypeCreate)
    {
        if (await CheckIfLeaveTypeNameExistsAsync(LeaveTypeCreate.Name))
        {
            ModelState.AddModelError(string.Empty, "A leave type with this name already exists in the system.");
        }

        if (ModelState.IsValid)
        {
            var leavetype = _mapper.Map<LeaveType>(LeaveTypeCreate);
            _context.Add(leavetype);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(LeaveTypeCreate);
    }

    public async Task<bool> CheckIfLeaveTypeNameExistsAsync(string name)
    {
        var lowerCaseName = name.ToLower();
        return await _context.LeaveTypes.AnyAsync(l => l.Name.ToLower() == lowerCaseName);
    }

    // GET: LEAVETYPES/Edit/5
    public async Task<IActionResult> Edit(int? id, LeaveTypesEditVM leaveTypeEdit)
    {
        if (await _leaveTypeService.CheckIfLeaveTypeNameExistsAsyncForEdit(leaveTypeEdit))
        {
            ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExistsValidationMessage);
        }

            if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _context.LeaveTypes.FindAsync(id);
        if (leavetype == null)
        {
            return NotFound();
        }

        var editVm = _mapper.Map<LeaveTypesEditVM>(leavetype);
        return View(editVm);
    }

    public async Task<bool> CheckIfLeaveTypeNameExistsAsyncForEdit(LeaveTypesEditVM leaveTypeEdit)
    {
        var lowerCaseName = leaveTypeEdit.Name.ToLower();
        return await _context.LeaveTypes.AnyAsync(l => l.Name.ToLower() == lowerCaseName && l.Id != leaveTypeEdit.Id);
    }

    // POST: LEAVETYPES/Edit
    // Accept the edit view model posted from the form. The Id comes from a hidden field in the form.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(LeaveTypesEditVM editVm)
    {
        if (editVm == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var leavetype = _mapper.Map<LeaveType>(editVm);
                _context.Update(leavetype);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _leaveTypeService.LeaveTypeExists(editVm.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(editVm);
    }

    // GET: LEAVETYPES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leavetype == null)
        {
            return NotFound();
        }

        var viewModel = _mapper.Map<LeaveTypeReadOnlyViewModel>(leavetype);
        return View(viewModel);
    }

    // POST: LEAVETYPES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var leavetype = await _context.LeaveTypes.FindAsync(id);
        if (leavetype != null)
        {
            _context.LeaveTypes.Remove(leavetype);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public bool LeaveTypeExists(int id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }
}
