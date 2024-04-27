namespace ChristCodingChallengeBackend
{
    public class Article(int artikelnummer, string marke, string material1, string material2, string material3,
        string legierung1, string legierung2, string legierung3, string kollektion, string warengruppe,
        string warenhauptgruppe, string geschlecht)
    {
        #region Fields
        // articleId
        private int _artikelnummer = artikelnummer;
        // MRK
        private string _marke = marke;
        // MAT
        private string _material1 = material1;
        // MAT2
        private string _material2 = material2;
        // MAT3
        private string _material3 = material3;
        // LEG
        private string _legierung1 = legierung1;
        // LEG2
        private string _legierung2 = legierung2;
        // LEG3
        private string _legierung3 = legierung3;
        // KOLL
        private string _kollektion = kollektion;
        // WRG_2
        private string _warengruppe = warengruppe;
        // WHG_2
        private string _warenhauptgruppe = warenhauptgruppe;
        // ZIEL
        private string _geschlecht = geschlecht;
        #endregion

        #region Propeties
        public int Artikelnummer { get; set; }
        public string Marke { get; set; } = string.Empty;
        public string Material1 { get; set; } = string.Empty;
        public string Material2 { get; set; } = string.Empty;
        public string Material3 { get; set; } = string.Empty;
        public string Legierung1 { get; set; } = string.Empty;
        public string Legierung2 { get; set; } = string.Empty;
        public string Legierung3 { get; set; } = string.Empty;
        public string Kollektion { get; set; } = string.Empty;
        public string Warengruppe { get; set; } = string.Empty;
        public string Warenhauptgruppe { get; set; } = string.Empty;
        public string Geschlecht { get; set; } = string.Empty;
        #endregion

        #region Contructors
        public Article(int artikelnummer) : this(artikelnummer, "", "", "", "", "", "", "", "", "", "", "")
        {

        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Artikelnummer: {_artikelnummer}, Marke: {_marke}, Material1: {_material1}, Material2:" +
                $"{_material2}, Material3: {_material3}, Legierung1: {_legierung1}, Legierung2: {_legierung2}," +
                $"Legierung3: {_legierung3}, Kollektion: {_kollektion}, Warengruppe: {_warengruppe}," +
                $"Warenhauptgruppe: {_warenhauptgruppe}, Geschlecht: {_geschlecht}";
        }

        public string ToCsv()
        {
            return $"{_artikelnummer};{_marke};{_material1};{_material2};{_material3};{_legierung1};{_legierung2};" +
                $"{_legierung3};{_kollektion};{_warengruppe};{_warenhauptgruppe};{_geschlecht}";
        }

        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                { "Artikelnummer", _artikelnummer.ToString() },
                { "Marke", _marke },
                { "Material1", _material1 },
                { "Material2", _material2 },
                { "Material3", _material3 },
                { "Legierung1", _legierung1 },
                { "Legierung2", _legierung2 },
                { "Legierung3", _legierung3 },
                { "Kollektion", _kollektion },
                { "Warengruppe", _warengruppe },
                { "Warenhauptgruppe", _warenhauptgruppe },
                { "Geschlecht", _geschlecht }
            };
        }
        #region Getters
        public int GetArtikelnummer()
        {
            return _artikelnummer;
        }
        public string GetMarke()
        {
            return _marke;
        }
        public string GetMaterial1()
        {
            return _material1;
        }
        public string GetMaterial2()
        {
            return _material2;
        }
        public string GetMaterial3()
        {
            return _material3;
        }
        public string GetLegierung1()
        {
            return _legierung1;
        }
        public string GetLegierung2()
        {
            return _legierung2;
        }
        public string GetLegierung3()
        {
            return _legierung3;
        }
        public string GetKollektion()
        {
            return _kollektion;
        }
        public string GetWarengruppe()
        {
            return _warengruppe;
        }
        public string GetWarenhauptgruppe()
        {
            return _warenhauptgruppe;
        }
        public string GetGeschlecht()
        {
            return _geschlecht;
        }
        #endregion

        #region Setters
        public void SetArtikelnummer(int artikelnummer)
        {
            _artikelnummer = artikelnummer;
        }
        public void SetMarke(string marke)
        {
            _marke = marke;
        }
        public void SetMaterial1(string material1)
        {
            _material1 = material1;
        }
        public void SetMaterial2(string material2)
        {
            _material2 = material2;
        }
        public void SetMaterial3(string material3)
        {
            _material3 = material3;
        }
        public void SetLegierung1(string legierung1)
        {
            _legierung1 = legierung1;
        }
        public void SetLegierung2(string legierung2)
        {
            _legierung2 = legierung2;
        }
        public void SetLegierung3(string legierung3)
        {
            _legierung3 = legierung3;
        }
        public void SetKollektion(string kollektion)
        {
            _kollektion = kollektion;
        }
        public void SetWarengruppe(string warengruppe)
        {
            _warengruppe = warengruppe;
        }
        public void SetWarenhauptgruppe(string warenhauptgruppe)
        {
            _warenhauptgruppe = warenhauptgruppe;
        }
        public void SetGeschlecht(string geschlecht)
        {
            _geschlecht = geschlecht;
        }
        #endregion

        #endregion
    }
}
