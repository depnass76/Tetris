using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest;

namespace Tetris.Unit_Tests.GridManager_Tests
{
    class spaceIsEmtpy_Test : UnitTestBase
    {
        public void spaceIsEmptyTest()
        {
            GridManager.CreateGridManager();

            bool test = GridManager.spaceIsEmpty(0, 0);

            CHECK(test == true);
        }
    }
}
