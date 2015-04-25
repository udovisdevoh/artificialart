using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Logic;
using ArtificialArt.Parsing;

namespace testing
{
    static class UnitTestDeriver
    {
        public static void TestAll()
        {
            #warning make sure == is ok (remove == from transaformations, add new -> <- transaformation from ==
            #warning could change operator to enum

            Deriver deriver = new Deriver();
            LogicDerivation derivation = deriver.Derive(new string[] { "P->Q", "Q&&F", "Q->(R||S)", "!R", "F->Z", "X->O&&J", "W||I->M" });

            IEnumerable<TreeExpression> proof;
            AssertEquals(deriver.TryProove(derivation, "Z", SolvingMethod.Straight, out proof), true);
            AssertEquals(deriver.TryProove(derivation, "Z", SolvingMethod.ReductioAdAbsurdum, out proof), true);
            
            AssertEquals(deriver.TryProove(derivation, "!Z", SolvingMethod.Straight, out proof), false);
            AssertEquals(deriver.TryProove(derivation, "!Z", SolvingMethod.ReductioAdAbsurdum, out proof), false);

            AssertEquals(deriver.TryProove(derivation, "X->O", SolvingMethod.Straight, out proof), true);
            AssertEquals(deriver.TryProove(derivation, "X->O", SolvingMethod.ReductioAdAbsurdum, out proof), true);

            AssertEquals(deriver.TryProove(derivation, "W->M", SolvingMethod.Straight, out proof), true);
            AssertEquals(deriver.TryProove(derivation, "W->M", SolvingMethod.ReductioAdAbsurdum, out proof), true);

            AssertEquals(deriver.TryProove(derivation, "X!>O", SolvingMethod.Straight, out proof), false);
            AssertEquals(deriver.TryProove(derivation, "X!>O", SolvingMethod.ReductioAdAbsurdum, out proof), false);

            AssertEquals(deriver.TryProove(derivation, "W!>M", SolvingMethod.Straight, out proof), false);
            AssertEquals(deriver.TryProove(derivation, "W!>M", SolvingMethod.ReductioAdAbsurdum, out proof), false);
        }

        private static void AssertEquals(bool bool1, bool bool2)
        {
            if (bool1 != bool2)
                throw new Exception("Both should be true or false");
        }
    }
}
