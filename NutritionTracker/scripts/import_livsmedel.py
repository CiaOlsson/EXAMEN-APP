import pandas as pd
import numpy as np
from sqlalchemy import create_engine

server = 'localhost'
database = 'NutritionTrackerDb'
driver = 'ODBC Driver 17 for SQL Server'

connection_string = f"mssql+pyodbc://@{server}/{database}?driver={driver.replace(' ', '+')}&trusted_connection=yes"
engine = create_engine(connection_string)

# === LÄS IN EXCELFIL ===
df = pd.read_excel("LivsmedelsDB_202504251254.xlsx", header=2)

# === STÄDA KOLUMNERNAS NAMN ===
df.columns = df.columns.str.strip()

# === MAPPNING AV KOLUMNNAMN ===
columns = {
    'Livsmedelsnamn': 'Name',
    'Livsmedelsnummer': 'FoodId',
    'Gruppering': 'Group',
    'Energi (kcal)': 'Energy_kcal',
    'Energi (kJ)': 'Energy_kj',
    'Fett, totalt (g)': 'FatTotal_g',
    'Protein (g)': 'Protein_g',
    'Kolhydrater, tillgängliga (g)': 'Carbohydrates_g',
    'Fibrer (g)': 'Fiber_g',
    'Vatten (g)': 'Water_g',
    'Alkohol (g)': 'Alcohol_g',
    'Aska (g)': 'Ash_g',
    'Sockerarter, totalt (g)': 'SugarsTotal_g',
    'Monosackarider (g)': 'Monosaccharides_g',
    'Disackarider (g)': 'Disaccharides_g',
    'Tillsatt socker (g)': 'AddedSugar_g',
    'Fritt socker (g)': 'FreeSugar_g',
    'Fullkorn totalt (g)': 'WholeGrainTotal_g',
    'Summa mättade fettsyror (g)': 'SaturatedFattyAcids_g',
    'Fettsyra 4:0-10:0 (g)': 'FattyAcids410_g',
    'Laurinsyra C12:0 (g)': 'LauricAcid_g',
    'Myristinsyra C14:0 (g)': 'MyristicAcid_g',
    'Palmitinsyra C16:0 (g)': 'PalmiticAcid_g',
    'Stearinsyra C18:0 (g)': 'StearicAcid_g',
    'Arakidinsyra C20:0 (g)': 'ArachidicAcid_g',
    'Summa enkelomättade fettsyror (g)': 'MonounsaturatedFattyAcids_g',
    'Palmitoljesyra C16:1 (g)': 'PalmitoleicAcid_g',
    'Oljesyra C18:1 (g)': 'OleicAcid_g',
    'Summa fleromättade fettsyror (g)': 'PolyunsaturatedFattyAcids_g',
    'Linolsyra C18:2 (g)': 'LinoleicAcid_g',
    'Linolensyra C18:3 (g)': 'LinolenicAcid_g',
    'Arakidonsyra C20:4 (g)': 'ArachidonicAcid_g',
    'EPA (C20:5) (g)': 'EPA_g',
    'DPA (C22:5) (g)': 'DPA_g',
    'DHA (C22:6) (g)': 'DHA_g',
    'Kolesterol (mg)': 'Cholesterol_mg',
    'Vitamin A (RE/µg)': 'Vitamin_A_Re_ug',
    'Retinol (µg)': 'Retinol_ug',
    'Betakaroten/β-Karoten (µg)': 'BetaCarotene_ug',
    'Vitamin D (µg)': 'Vitamin_D_ug',
    'Vitamin E (mg)': 'Vitamin_E_mg',
    'Vitamin K (µg)': 'Vitamin_K_ug',
    'Tiamin (mg)': 'Thiamin_mg',
    'Riboflavin (mg)': 'Riboflavin_mg',
    'Niacin (mg)': 'Niacin_mg',
    'Niacinekvivalenter (NE/mg)': 'NiacinEquivalents_mg',
    'Vitamin B6 (mg)': 'Vitamin_B6_mg',
    'Folat, totalt (µg)': 'FolateTotal_ug',
    'Vitamin B12 (µg)': 'Vitamin_B12_ug',
    'Vitamin C (mg)': 'Vitamin_C_mg',
    'Fosfor, P (mg)': 'Phosphorus_mg',
    'Jod, I (µg)': 'Iodine_ug',
    'Järn, Fe (mg)': 'Iron_mg',
    'Kalcium, Ca (mg)': 'Calcium_mg',
    'Kalium, K (mg)': 'Potassium_mg',
    'Magnesium, Mg (mg)': 'Magnesium_mg',
    'Natrium, Na (mg)': 'Sodium_mg',
    'Salt, NaCl (g)': 'Salt_g',
    'Selen, Se (µg)': 'Selenium_ug',
    'Zink, Zn (mg)': 'Zinc_mg',
    'Avfall (skal etc.) (%)': 'Waste_percent'
}

# === BYT NAMN
df = df.rename(columns=columns)

# === KOLLA vilka kolumner som finns
existing_columns = [col for col in columns.values() if col in df.columns]
missing_columns = [col for col in columns.keys() if columns[col] not in df.columns]

# === Skriv ut resultat
print(f"Hittade {len(existing_columns)} kolumner som matchar.")
if missing_columns:
    print(f"Saknade kolumner i Excel: {missing_columns}")

# === Ta bara de kolumner som finns
df = df[existing_columns]

df = df.astype({
    'FoodId': np.int32,
    'Energy_kcal': np.int32,
    'Energy_kj': np.int32,
    'AddedSugar_g': np.int32,
    'FreeSugar_g': np.int32,
    'WholeGrainTotal_g': np.int32,
    'Phosphorus_mg': np.int32,
    'Calcium_mg': np.int32,
    'Potassium_mg': np.int32,
    'Magnesium_mg': np.int32,
    'Sodium_mg': np.int32
    })

# === SPARA TILL SQL
df.to_sql('Foods', con=engine, if_exists='replace', index=False)

print("Importen är klar!")
