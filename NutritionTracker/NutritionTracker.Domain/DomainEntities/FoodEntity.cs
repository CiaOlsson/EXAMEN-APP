using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Domain.DomainEntities
{
	public class FoodEntity
	{
		//Jag tar med alla värden tillsvidare. Kanske skalar ner senare. 
		[Key]
		public int FoodId { get; set; }
		public string Name { get; set; }
		public string Group { get; set; }
		public int? Energy_kcal { get; set; }
		public int? Energy_kj { get; set; }
		public float? FatTotal_g { get; set; }
		public float? Protein_g { get; set; }
		public float? Carbohydrates_g { get; set; }
		public float? Fiber_g { get; set; }
		public float? Water_g { get; set; }
		public float? Alcohol_g { get; set; }
		public float? Ash_g { get; set; }
		public float? SugarsTotal_g { get; set; }
		public float? Monosaccharides_g { get; set; }
		public float? Disaccharides_g { get; set; }
		public int? AddedSugar_g { get; set; }
		public int? FreeSugar_g { get; set; }
		public int? WholeGrainTotal_g { get; set; }
		public float? SaturatedFattyAcids_g { get; set; }
		public float? FattyAcids410_g { get; set; }
		public float? LauricAcid_g { get; set; }
		public float? MyristicAcid_g { get; set; }
		public float? PalmiticAcid_g { get; set; }
		public float? StearicAcid_g { get; set; }
		public float? ArachidicAcid_g { get; set; }
		public float? MonounsaturatedFattyAcids_g { get; set; }
		public float? PalmitoleicAcid_g { get; set; }
		public float? OleicAcid_g { get; set; }
		public float? PolyunsaturatedFattyAcids_g { get; set; }
		public float? LinoleicAcid_g { get; set; }
		public float? LinolenicAcid_g { get; set; }
		public float? ArachidonicAcid_g { get; set; }
		public float? EPA_g { get; set; }
		public float? DPA_g { get; set; }
		public float? DHA_g { get; set; }
		public float? Cholesterol_mg { get; set; }
		public float? Vitamin_A_Re_ug { get; set; }
		public float? Retinol_ug { get; set; }
		public float? BetaCarotene_ug { get; set; }
		public float? Vitamin_D_ug { get; set; }
		public float? Vitamin_E_mg { get; set; }
		public float? Vitamin_K_ug { get; set; }
		public float? Thiamin_mg { get; set; }
		public float? Riboflavin_mg { get; set; }
		public float? Niacin_mg { get; set; }
		public float? NiacinEquivalents_mg { get; set; }
		public float? Vitamin_B6_mg { get; set; }
		public float? FolateTotal_ug { get; set; }
		public float? Vitamin_B12_ug { get; set; }
		public float? Vitamin_C_mg { get; set; }
		public int? Phosphorus_mg { get; set; }
		public float? Iodine_ug { get; set; }
		public float? Iron_mg { get; set; }
		public int? Calcium_mg { get; set; }
		public int? Potassium_mg { get; set; }
		public int? Magnesium_mg { get; set; }
		public int? Sodium_mg { get; set; }
		public float? Salt_g { get; set; }
		public float? Selenium_ug { get; set; }
		public float? Zinc_mg { get; set; }
		public float? Waste_percent { get; set; }
	}
}
