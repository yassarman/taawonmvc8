using Abp.Web.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaawonMVC.Buildings;
using TaawonMVC.Buildings.DTO;
using TaawonMVC.BuildingType;
using TaawonMVC.BuildingType.DTO;
using TaawonMVC.Web.Models.Building;
using TaawonMVC.Models;
using TaawonMVC.Neighborhood;
using TaawonMVC.Neighborhood.DTO;
using TaawonMVC.BuildingUses;

namespace TaawonMVC.Web.Controllers
{
    public class BuildingController : AbpController
    {
        private readonly IBuildingsAppService _buildingsAppService;
        private readonly IBuildingTypeAppService _buildingTypeAppService;
        private readonly INeighborhoodAppService _neighborhoodAppService;
        private readonly IBuildingUsesAppService _buildingUsesAppService;
       
        public BuildingController(IBuildingsAppService buildingsAppService, IBuildingTypeAppService buildingTypeAppService, INeighborhoodAppService neighborhoodAppService, IBuildingUsesAppService buildingUsesAppService)
        {
            _buildingsAppService = buildingsAppService;
            _buildingTypeAppService = buildingTypeAppService;
            _neighborhoodAppService = neighborhoodAppService;
            _buildingUsesAppService = buildingUsesAppService;

        }
        // GET: Building
        public ActionResult Index()
        {
            var getBuildingOutput = _buildingsAppService.getAllBuildings();
            // if neighborhood or buildingtype is deleted 
            // initiate the object with default values .
            foreach(var Building  in getBuildingOutput)
            {
                if(Building.BuildingType==null)
                {
                    Building.BuildingType = new TaawonMVC.Models.BuildingType();
                }
                if(Building.NeighboorHood==null)
                {
                    Building.NeighboorHood = new TaawonMVC.Models.Neighborhood();
                }

            }
            //get the list of buildingTypes
            var buildingTypes = _buildingTypeAppService.getAllBuildingtype().ToList();
            // get the list of neighborhoods
            var neighborhoods = _neighborhoodAppService.GetAllNeighborhood().ToList();
            // get the list of BuildingUses
            var buildingUses = _buildingUsesAppService.getAllBuildingUses().ToList();

            //var BuildingTypeInput = new GetBuidlingTypeInput
            //{
            //    Id = id
            //};
            //var buildingType = _buildingTypeAppService.getBuildingTypeById(BuildingTypeInput);

            //populate yes-no list 
            var yesOrNo = new List<string>();
            yesOrNo.Add("True");
            yesOrNo.Add("False");

            //Read HoushName From Database 
            //----------------------------
            var HoushNames = new List<string>();
            foreach(var HoushName in getBuildingOutput)
            {
                if (!String.IsNullOrWhiteSpace(HoushName.houshName))
                {
                    HoushNames.Add(HoushName.houshName);
                }
            }
            var HoushNameArray = HoushNames.ToArray();
            //foreach(var str in HoushNameArray)
            //{
            //    str.Trim();
            //    str.ToUpper();
            //}
            //var houshNameArrayClean = new string[HoushNameArray.Length];
            //for(int i=0;i< HoushNameArray.Length;i++)
            //{
            //    HoushNameArray[i].Trim();
            //    HoushNameArray[i].ToUpper();
            //    houshNameArrayClean[i] = HoushNameArray[i];
            //}
            // var HoushNameArrayWithoutDuplicate = new HashSet<string>(HoushNameArray);
            var HoushNameArrayWithoutDuplicate = HoushNameArray.Distinct().ToArray();

            //----------------------------
            //-----------------------------

            var BuildingsViewModel = new BuildingViewModel
            {
                Buildings = getBuildingOutput,
                BuildingTypes = buildingTypes,
                Neighborhoods=neighborhoods,
                Building = new GetBuildingsOutput(),
                YesOrNo= new SelectList(yesOrNo),
                HoushNameArray= HoushNameArrayWithoutDuplicate,
                BuildingUses=buildingUses






            };
            return View("Index",BuildingsViewModel);
        }


        public ActionResult EditBuildingModal(int userId)
        {
            //get the list of buildingTypes
            var buildingTypes = _buildingTypeAppService.getAllBuildingtype().ToList();
            // get the list of neighborhoods
            var neighborhoods = _neighborhoodAppService.GetAllNeighborhood().ToList();

            var getBuidlingsInput = new GetBuidlingsInput
            {
                Id = userId
            };

            var getBuildingOutput = _buildingsAppService.getBuildingsById(getBuidlingsInput);

            var BuildingViewModel = new BuildingViewModel
            {
               Building = getBuildingOutput,
               BuildingTypes=buildingTypes,
               Neighborhoods=neighborhoods
               
            };

            return View("_EditUserModal", BuildingViewModel);

        }
    }
}