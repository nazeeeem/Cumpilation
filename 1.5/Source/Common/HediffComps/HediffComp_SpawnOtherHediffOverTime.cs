﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Cumpilation.Common
{
    public class HediffComp_SpawnOtherHediffOverTime : HediffComp
    {
        public HediffCompProperties_SpawnOtherHediffOverTime Props => (HediffCompProperties_SpawnOtherHediffOverTime)this.props;

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (this.props == null || parent == null || parent.pawn == null || !parent.pawn.Spawned) return;

            if (this.props != null && parent.pawn.IsHashIntervalTick(Props.tickInterval) && Props.IsValidPawn(parent.pawn)) {
                if ((new Random()).NextDouble() <= Props.applicationChance){
                    Hediff spawnedHediff = parent.pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediff);
                    if (spawnedHediff == null)
                    {
                        BodyPartRecord bpr = Props.TryToSpawnInSameBodyPart ? parent.Part : null;
                        spawnedHediff = HediffMaker.MakeHediff(Props.hediff, parent.pawn, bpr);
                        Pawn.health.AddHediff(spawnedHediff);
                    }
                    spawnedHediff.Severity += Props.severityIncrease;
                }
            }
        }
    }
}
