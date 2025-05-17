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
		public string? Group { get; set; }
		public int? Energy_kcal { get; set; }
		public int? Energy_kj { get; set; }
		public double? FatTotal_g { get; set; }
		public double? Protein_g { get; set; }
		public double? Carbohydrates_g { get; set; }
		public double? Fiber_g { get; set; }
		public double? Water_g { get; set; }
		public double? Alcohol_g { get; set; }
		public double? Ash_g { get; set; }
		public double? SugarsTotal_g { get; set; }
		public double? Monosaccharides_g { get; set; }
		public double? Disaccharides_g { get; set; }
		public int? AddedSugar_g { get; set; }
		public int? FreeSugar_g { get; set; }
		public int? WholeGrainTotal_g { get; set; }
		public double? SaturatedFattyAcids_g { get; set; }
		public double? FattyAcids410_g { get; set; }
		public double? LauricAcid_g { get; set; }
		public double? MyristicAcid_g { get; set; }
		public double? PalmiticAcid_g { get; set; }
		public double? StearicAcid_g { get; set; }
		public double? ArachidicAcid_g { get; set; }
		public double? MonounsaturatedFattyAcids_g { get; set; }
		public double? PalmitoleicAcid_g { get; set; }
		public double? OleicAcid_g { get; set; }
		public double? PolyunsaturatedFattyAcids_g { get; set; }
		public double? LinoleicAcid_g { get; set; }
		public double? LinolenicAcid_g { get; set; }
		public double? ArachidonicAcid_g { get; set; }
		public double? EPA_g { get; set; }
		public double? DPA_g { get; set; }
		public double? DHA_g { get; set; }
		public double? Cholesterol_mg { get; set; }
		public double? Vitamin_A_Re_ug { get; set; }
		public double? Retinol_ug { get; set; }
		public double? BetaCarotene_ug { get; set; }
		public double? Vitamin_D_ug { get; set; }
		public double? Vitamin_E_mg { get; set; }
		public double? Vitamin_K_ug { get; set; }
		public double? Thiamin_mg { get; set; }
		public double? Riboflavin_mg { get; set; }
		public double? Niacin_mg { get; set; }
		public double? NiacinEquivalents_mg { get; set; }
		public double? Vitamin_B6_mg { get; set; }
		public double? FolateTotal_ug { get; set; }
		public double? Vitamin_B12_ug { get; set; }
		public double? Vitamin_C_mg { get; set; }
		public int? Phosphorus_mg { get; set; }
		public double? Iodine_ug { get; set; }
		public double? Iron_mg { get; set; }
		public int? Calcium_mg { get; set; }
		public int? Potassium_mg { get; set; }
		public int? Magnesium_mg { get; set; }
		public int? Sodium_mg { get; set; }
		public double? Salt_g { get; set; }
		public double? Selenium_ug { get; set; }
		public double? Zinc_mg { get; set; }
		public double? Waste_percent { get; set; }
	}
}
