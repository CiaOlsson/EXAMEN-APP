import pandas as pd
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
    'Livsmedelsnamn': 'name',
    'Livsmedelsnummer': 'food_id',
    'Gruppering': 'group',
    'Energi (kcal)': 'energy_kcal',
    'Energi (kJ)': 'energy_kj',
    'Fett, totalt (g)': 'fat_total_g',
    'Protein (g)': 'protein_g',
    'Kolhydrater, tillgängliga (g)': 'carbohydrates_g',
    'Fibrer (g)': 'fiber_g',
    'Vatten (g)': 'water_g',
    'Alkohol (g)': 'alcohol_g',
    'Aska (g)': 'ash_g',
    'Sockerarter, totalt (g)': 'sugars_total_g',
    'Monosackarider (g)': 'monosaccharides_g',
    'Disackarider (g)': 'disaccharides_g',
    'Tillsatt socker (g)': 'added_sugar_g',
    'Fritt socker (g)': 'free_sugar_g',
    'Fullkorn totalt (g)': 'whole_grain_total_g',
    'Summa mättade fettsyror (g)': 'saturated_fatty_acids_g',
    'Fettsyra 4:0-10:0 (g)': 'fatty_acids_4_10_g',
    'Laurinsyra C12:0 (g)': 'lauric_acid_g',
    'Myristinsyra C14:0 (g)': 'myristic_acid_g',
    'Palmitinsyra C16:0 (g)': 'palmitic_acid_g',
    'Stearinsyra C18:0 (g)': 'stearic_acid_g',
    'Arakidinsyra C20:0 (g)': 'arachidic_acid_g',
    'Summa enkelomättade fettsyror (g)': 'monounsaturated_fatty_acids_g',
    'Palmitoljesyra C16:1 (g)': 'palmitoleic_acid_g',
    'Oljesyra C18:1 (g)': 'oleic_acid_g',
    'Summa fleromättade fettsyror (g)': 'polyunsaturated_fatty_acids_g',
    'Linolsyra C18:2 (g)': 'linoleic_acid_g',
    'Linolensyra C18:3 (g)': 'linolenic_acid_g',
    'Arakidonsyra C20:4 (g)': 'arachidonic_acid_g',
    'EPA (C20:5) (g)': 'epa_g',
    'DPA (C22:5) (g)': 'dpa_g',
    'DHA (C22:6) (g)': 'dha_g',
    'Kolesterol (mg)': 'cholesterol_mg',
    'Vitamin A (RE/µg)': 'vitamin_a_re_ug',
    'Retinol (µg)': 'retinol_ug',
    'Betakaroten/β-Karoten (µg)': 'beta_carotene_ug',
    'Vitamin D (µg)': 'vitamin_d_ug',
    'Vitamin E (mg)': 'vitamin_e_mg',
    'Vitamin K (µg)': 'vitamin_k_ug',
    'Tiamin (mg)': 'thiamin_mg',
    'Riboflavin (mg)': 'riboflavin_mg',
    'Niacin (mg)': 'niacin_mg',
    'Niacinekvivalenter (NE/mg)': 'niacin_equivalents_mg',
    'Vitamin B6 (mg)': 'vitamin_b6_mg',
    'Folat, totalt (µg)': 'folate_total_ug',
    'Vitamin B12 (µg)': 'vitamin_b12_ug',
    'Vitamin C (mg)': 'vitamin_c_mg',
    'Fosfor, P (mg)': 'phosphorus_mg',
    'Jod, I (µg)': 'iodine_ug',
    'Järn, Fe (mg)': 'iron_mg',
    'Kalcium, Ca (mg)': 'calcium_mg',
    'Kalium, K (mg)': 'potassium_mg',
    'Magnesium, Mg (mg)': 'magnesium_mg',
    'Natrium, Na (mg)': 'sodium_mg',
    'Salt, NaCl (g)': 'salt_g',
    'Selen, Se (µg)': 'selenium_ug',
    'Zink, Zn (mg)': 'zinc_mg',
    'Avfall (skal etc.) (%)': 'waste_percent'
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

# === SPARA TILL SQL
df.to_sql('Foods', con=engine, if_exists='replace', index=False)

print("Importen är klar!")
