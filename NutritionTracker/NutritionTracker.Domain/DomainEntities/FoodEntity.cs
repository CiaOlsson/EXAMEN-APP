using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Domain.DomainEntities
{
	public class FoodEntity
	{
		//Jag tar med alla värden tillsvidare. Kanske skalar ner senare. 
		public int FoodId { get; set; }
		public string Name { get; set; }
		public string Group { get; set; }
		public float? EnergyKcal { get; set; }
		public float? EnergyKj { get; set; }
		public float? FatTotalG { get; set; }
		public float? ProteinG { get; set; }
		public float? CarbohydratesG { get; set; }
		public float? FiberG { get; set; }
		public float? WaterG { get; set; }
		public float? AlcoholG { get; set; }
		public float? AshG { get; set; }
		public float? SugarsTotalG { get; set; }
		public float? MonosaccharidesG { get; set; }
		public float? DisaccharidesG { get; set; }
		public float? AddedSugarG { get; set; }
		public float? FreeSugarG { get; set; }
		public float? WholeGrainTotalG { get; set; }
		public float? SaturatedFattyAcidsG { get; set; }
		public float? FattyAcids410G { get; set; }
		public float? LauricAcidG { get; set; }
		public float? MyristicAcidG { get; set; }
		public float? PalmiticAcidG { get; set; }
		public float? StearicAcidG { get; set; }
		public float? ArachidicAcidG { get; set; }
		public float? MonounsaturatedFattyAcidsG { get; set; }
		public float? PalmitoleicAcidG { get; set; }
		public float? OleicAcidG { get; set; }
		public float? PolyunsaturatedFattyAcidsG { get; set; }
		public float? LinoleicAcidG { get; set; }
		public float? LinolenicAcidG { get; set; }
		public float? ArachidonicAcidG { get; set; }
		public float? EPAg { get; set; }
		public float? DPAg { get; set; }
		public float? DHAg { get; set; }
		public float? CholesterolMg { get; set; }
		public float? VitaminAReUg { get; set; }
		public float? RetinolUg { get; set; }
		public float? BetaCaroteneUg { get; set; }
		public float? VitaminDUg { get; set; }
		public float? VitaminEMg { get; set; }
		public float? VitaminKug { get; set; }
		public float? ThiaminMg { get; set; }
		public float? RiboflavinMg { get; set; }
		public float? NiacinMg { get; set; }
		public float? NiacinEquivalentsMg { get; set; }
		public float? VitaminB6Mg { get; set; }
		public float? FolateTotalUg { get; set; }
		public float? VitaminB12Ug { get; set; }
		public float? VitaminCMg { get; set; }
		public float? PhosphorusMg { get; set; }
		public float? IodineUg { get; set; }
		public float? IronMg { get; set; }
		public float? CalciumMg { get; set; }
		public float? PotassiumMg { get; set; }
		public float? MagnesiumMg { get; set; }
		public float? SodiumMg { get; set; }
		public float? SaltG { get; set; }
		public float? SeleniumUg { get; set; }
		public float? ZincMg { get; set; }
		public float? WastePercent { get; set; }
	}
}
