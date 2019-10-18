using ItemHoarder.Data;
using ItemHoarder.Data.Characteristics.Features;
using ItemHoarder.Models.Characteristics.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Services.Characteristics
{
    public class FeatureService
    {
        private readonly Guid _userId;
        public FeatureService(Guid userId)
        {
            _userId = userId;
        }
        //see all
        public IEnumerable<FeatureIndex> GetAllFeatures()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var features = ctx.Features.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(f => new FeatureIndex
                {
                    FeatureID = f.FeatureID,
                    OwnerID = f.OwnerID,
                    GameType = f.GameType,
                    FeatureName = f.FeatureName,
                    Description = f.Description,
                    RacePointCost = f.RacePointCost,
                    DateOfCreation = f.DateOfCreation,
                    DateOfModification = f.DateOfModification
                }).ToArray();
                return features;
            }
        }
        //see by ID
        public FeatureDetails GetFeatureById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var feature = ctx.Features.SingleOrDefault(e => e.OwnerID == _userId && e.FeatureID == id);
                if (feature != null)
                {
                    Dictionary<string, int> stats = new Dictionary<string, int>();
                    var split = feature.StatPrerequisite.Split('|');
                    foreach (var item in split)
                    {
                        var some = item.Split(' ');
                        stats.Add(some[0], int.Parse(some[1]));
                    }
                    return new FeatureDetails
                    {
                        FeatureID = feature.FeatureID,
                        GameType = feature.GameType,
                        FeatureName = feature.FeatureName,
                        Description = feature.Description,
                        Benefits = feature.Benefits,
                        Special = feature.Special,
                        RacePointCost = feature.RacePointCost,
                        RaceIdPrerequisite = new Dictionary<int, string>(),
                        //ClassIdPrerequisite,
                        FeatureIdPrerequisite = new Dictionary<int, string>(),
                        StatPrerequisite = stats,
                        LvlPrerequisite = feature.LvlPrerequisite,
                        StrengthModifier = feature.StrengthModifier,
                        DexterityModifier = feature.DexterityModifier,
                        ConstitutionModifier = feature.ConstitutionModifier,
                        IntelligenceModifier = feature.IntelligenceModifier,
                        WisdomModifier = feature.WisdomModifier,
                        CharismaModifier = feature.CharismaModifier,
                        DateOfCreation = feature.DateOfCreation,
                        DateOfModification = feature.DateOfModification
                    };
                }
                else return null;
            }
        }
        //create
        public bool CreateFeature(FeatureCreate newFeature)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var feature = new Feature
                {
                    OwnerID = _userId,
                    GameType = newFeature.GameType,
                    RacePointCost = newFeature.RacePointCost,
                    FeatureName = newFeature.FeatureName,
                    Description = newFeature.Description,
                    Benefits = newFeature.Benefits,
                    Special = newFeature.Special,
                    RaceIDPrerequisites = new List<FRacePrerequisites>(),
                    //ClassIDPrerequisites,
                    FeatureIDPrerequisites = new List<FFeatPrerequisites>(),
                    StatPrerequisite = String.Join("|", newFeature.StatPrerequisite.Select(s => s.Key + " " + s.Value).ToArray()),
                    LvlPrerequisite = newFeature.LvlPrerequisite,
                    StrengthModifier = newFeature.StrengthModifier,
                    DexterityModifier = newFeature.DexterityModifier,
                    ConstitutionModifier = newFeature.ConstitutionModifier,
                    IntelligenceModifier = newFeature.IntelligenceModifier,
                    WisdomModifier = newFeature.WisdomModifier,
                    CharismaModifier = newFeature.CharismaModifier,
                    IsDeactivated = false,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                if (newFeature.RaceIdPrerequisite != null) newFeature.RaceIdPrerequisite.ForEach(e => feature.RaceIDPrerequisites.Add(new FRacePrerequisites { RaceID = e }));
                if (newFeature.FeatureIdPrerequisite != null) newFeature.FeatureIdPrerequisite.ForEach(e => feature.FeatureIDPrerequisites.Add(new FFeatPrerequisites { FeatureID = e }));

                ctx.Features.Add(feature);
                return ctx.SaveChanges() == 1;
            }
        }
        //edit
        public bool UpdateFeature(FeatureDetails edit)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var feat = ctx.Features.SingleOrDefault(e => e.OwnerID == _userId && e.FeatureID == edit.FeatureID);
                if (feat != null)
                {
                    feat.GameType = edit.GameType;
                    feat.RacePointCost = edit.RacePointCost;
                    feat.FeatureName = edit.FeatureName;
                    feat.Description = edit.Description;
                    feat.Benefits = edit.Benefits;
                    feat.Special = edit.Special;
                    //feat.RaceIDPrerequisites;
                    //feat.ClassIDPrerequisites;
                    //feat.FeatureIDPrerequisites;
                    feat.StatPrerequisite = String.Join("|", edit.StatPrerequisite.Select(s => s.Key + " " + s.Value).ToArray());
                    feat.LvlPrerequisite = edit.LvlPrerequisite;
                    feat.StrengthModifier = edit.StrengthModifier;
                    feat.DexterityModifier = edit.DexterityModifier;
                    feat.ConstitutionModifier = edit.ConstitutionModifier;
                    feat.IntelligenceModifier = edit.IntelligenceModifier;
                    feat.WisdomModifier = edit.WisdomModifier;
                    feat.CharismaModifier = edit.CharismaModifier;
                    feat.DateOfModification = DateTimeOffset.UtcNow;

                    return ctx.SaveChanges() == 1;
                }
                else return false;
            }
        }
        //"delete"
        public bool DeleteFeature(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var feat = ctx.Features.Single(e => e.OwnerID == _userId && e.FeatureID == id);
                feat.IsDeactivated = true;
                return ctx.SaveChanges() == 1;
            }
        }

        //Remove&Add feat from feat
        public bool AddOrRemoveRaceOnFeature(bool areRemoving, int featId, int raceId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                if (areRemoving)
                {
                    var feature = ctx.Features.Include("RaceIDPrerequisites").Single(e => e.OwnerID == _userId && e.FeatureID == featId);
                    feature.RaceIDPrerequisites.Remove(feature.RaceIDPrerequisites.First(e => e.RaceID == raceId));
                    return ctx.SaveChanges() == 2;
                }
                else
                {
                    var feature = ctx.Features.Include("RaceIDPrerequisites").Single(e => e.OwnerID == _userId && e.FeatureID == featId);
                    feature.RaceIDPrerequisites.Add(new FRacePrerequisites { RaceID = raceId });
                    return ctx.SaveChanges() == 2;
                }
            }
        }
        //Remove&Add Race from feat
        public bool AddOrRemoveFeatureOnFeature(bool areRemoving, int featId, int secondaryFeatId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                if (areRemoving)
                {
                    var feature = ctx.Features.Include("FeatureIDPrerequisites").Single(e => e.OwnerID == _userId && e.FeatureID == featId);
                    feature.FeatureIDPrerequisites.Remove(feature.FeatureIDPrerequisites.First(e => e.FeatureID == secondaryFeatId));
                    return ctx.SaveChanges() == 2;
                }
                else
                {
                    var feature = ctx.Features.Include("FeatureIDPrerequisites").Single(e => e.OwnerID == _userId && e.FeatureID == featId);
                    feature.FeatureIDPrerequisites.Add(new FFeatPrerequisites { FeatureID = secondaryFeatId });
                    return ctx.SaveChanges() == 2;
                }
            }
        }
        //Remove&Add class from feat
    }
}
