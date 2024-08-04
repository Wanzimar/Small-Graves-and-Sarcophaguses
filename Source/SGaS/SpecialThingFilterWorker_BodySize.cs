﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SGaS
{
    internal class SpecialThingFilterWorker_BodySize : SpecialThingFilterWorker
    {        
                
        public override bool Matches(Thing t) //a check for a particular corpse to see if it fits the criteria?
        {
            if (t is Corpse corpse)
            {
                return corpse.InnerPawn.BodySize > SGaSMod.instance.Settings.BodySizeMin;
            }
            return false;
        }

        public override bool CanEverMatch(ThingDef def)
        {
            return AlwaysMatches(def);// I don't understand this part, if you do I'd be grateful if you could explaine it to me)
            //for some reason, this line seems to make it so there is no button to enable/disable this filter in the sarcofagus/grave "storage" tab, why?
            //if a proper condition is written, the button appears (on regular sarcofaguses/graves, since the filter is dissallowed on small through xml)
        }

        public override bool AlwaysMatches(ThingDef def) //a check for an entire race to see if all corpses from it will always fit the criteria?
        {                                                //responsible for race's appearance in the "storage" tab 
            if (def.IsCorpse && def.ingestible?.sourceDef?.race != null)
            {
                RaceProperties race = def.ingestible.sourceDef.race;
                for (int i = 0; i < race.lifeStageAges.Count; i++)
                {
                    if (race.lifeStageAges[i].def.bodySizeFactor * race.baseBodySize <= SGaSMod.instance.Settings.BodySizeMin)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

    }
}