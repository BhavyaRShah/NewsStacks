//using NewsStacks.IService;
//using NewsStacks.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace NewsStacks.Service
//{
//    public class TimezoneService : ITimezoneService
//    {
//        readonly NewsStacksContext _dbContext;

//        public TimezoneService(NewsStacksContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        ///<summary>
//        ///To get timezone by id
//        ///</summary>
//        public Timezone GetById(int timezoneId)
//        {
//            if (timezoneId != 0)
//            {
//                Timezone timezone = _dbContext.Timezones.Where(x => x.Timezoneid == timezoneId).FirstOrDefault();
//                return timezone;
//            }
//            else
//                return null;
//        }

//        ///<summary>
//        ///To get list of timezones to the criteria
//        ///</summary>
//        public IEnumerable<Timezone> GetList(int page = 0, int size = 0, string search = "")
//        {
//            var timezones = _dbContext.Timezones.ToList();
//            return timezones;
//        }
//    }
//}
