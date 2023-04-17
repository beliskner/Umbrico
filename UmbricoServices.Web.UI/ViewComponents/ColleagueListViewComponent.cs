using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UmbricoServices.Web.UI.Services;

namespace UmbricoServices.Web.UI.ViewComponents;

public class ColleagueListViewComponent : ViewComponent
{
    private readonly IColleagueService _colleagueService;

    public ColleagueListViewComponent(IColleagueService colleagueService)
    {
        _colleagueService = colleagueService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var colleagues = await _colleagueService.GetAllColleagues();
        return View(colleagues);
    }
}