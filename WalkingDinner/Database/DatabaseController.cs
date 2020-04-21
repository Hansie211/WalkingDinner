using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingDinner.Models;

namespace WalkingDinner.Database {

    public class DatabaseController {

        private readonly DatabaseContext Database;

        public DatabaseController( DatabaseContext database ) {

            Database = database;
        }

        public Dinner CreateDinner( Dinner dinner ) {

            dinner.AdminCode = dinner.GenerateCode();
            dinner.Code = dinner.GenerateCode();

            return null;
        }
    }
}
