using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategoryAPI.Data;
using ProductCategoryAPI.Dtos;
using ProductCategoryAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly AppDbContext _context;

    public MenuController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
    {
        var menus = await _context.Menus
            .Include(m => m.Children)
            .Where(m => m.ParentId == null)
            .ToListAsync();

        var dto = menus.Select(m => MapToDto(m)).ToList();
        return Ok(dto);
    }
    
    private MenuDto MapToDto(Menu menu)
    {
        return new MenuDto
        {
            Id = menu.Id,
            Name = menu.Name,
            ParentId = menu.ParentId,
            Children = menu.Children?.Select(c => MapToDto(c)).ToList() ?? new List<MenuDto>()
        };
    }
}
