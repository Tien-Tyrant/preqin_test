import sqlite3
import csv

# Connect to SQLite database (creates a new one if it doesn't exist)
conn = sqlite3.connect('preqin.db')
cursor = conn.cursor()

# Create Investors table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Investors (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Type TEXT NOT NULL,
    Country TEXT NOT NULL,
    DateAdded TEXT NOT NULL,
    LastUpdated TEXT NOT NULL
);
''')

# Create AssetClasses table
cursor.execute('''
CREATE TABLE IF NOT EXISTS AssetClasses (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE
);
''')

# Create Commitments table
cursor.execute('''
CREATE TABLE IF NOT EXISTS Commitments (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    InvestorId INTEGER NOT NULL,
    AssetClassId INTEGER NOT NULL,
    Amount REAL NOT NULL,
    Currency TEXT NOT NULL,
    FOREIGN KEY (InvestorId) REFERENCES Investors(Id),
    FOREIGN KEY (AssetClassId) REFERENCES AssetClasses(Id)
);
''')

# Function to add investors, asset classes, and commitments from CSV data
def add_data_from_csv(csv_file):
    with open(csv_file, 'r') as file:
        csv_reader = csv.DictReader(file)
        investors = {}
        asset_classes = {}
        
        for row in csv_reader:
            # Check if the investor already exists
            name = row['Investor Name']
            if name not in investors:
                cursor.execute('''
                INSERT INTO Investors (Name, Type, Country, DateAdded, LastUpdated)
                VALUES (?, ?, ?, ?, ?)
                ''', (row['Investor Name'], row['Investory Type'], row['Investor Country'], row['Investor Date Added'], row['Investor Last Updated']))
                investors[name] = cursor.lastrowid
            
            # Check if the asset class already exists
            asset_class = row['Commitment Asset Class']
            if asset_class not in asset_classes:
                cursor.execute('''
                INSERT INTO AssetClasses (Name)
                VALUES (?)
                ''', (asset_class,))
                asset_classes[asset_class] = cursor.lastrowid
            
            # Insert each commitment linked to the investor and asset class
            cursor.execute('''
            INSERT INTO Commitments (InvestorId, AssetClassId, Amount, Currency)
            VALUES (?, ?, ?, ?)
            ''', (investors[name], asset_classes[asset_class], float(row['Commitment Amount']), row['Commitment Currency']))

# Seed data from CSV
add_data_from_csv('data.csv')

# Commit changes and close connection
conn.commit()
conn.close()

print("Database created and data inserted successfully.")
