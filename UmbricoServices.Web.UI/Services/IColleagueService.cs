using System.Threading.Tasks;
using UmbricoServices.Web.UI.ViewModels;

namespace UmbricoServices.Web.UI.Services;

public interface IColleagueService
{
    Task<ColleagueViewModel[]> GetAllColleagues();
    Task<ColleagueViewModel?> GetColleagueByName(string name);
    Task<bool> DeleteColleagueById(string id);
    Task<ColleagueViewModel?> AddColleague(ColleagueViewModel colleagueViewModel);
}