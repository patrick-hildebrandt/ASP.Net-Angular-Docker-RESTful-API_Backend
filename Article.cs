using System;

namespace ChristCodingChallengeBackend
{
    public class Article(string artikelnummer, string marke, string material1, string material2, string material3,
        string legierung1, string legierung2, string legierung3, string kollektion, string warengruppe,
        string warenhauptgruppe, string geschlecht)
    {
        #region Fields
        // tatsächliche Attribute:
        // articleId
        // Artikelnummer - MITMAS_ITNO
        // Marke - MRK
        // Material 1 - MAT
        // Legierung 1 - LEG
        // Kollektionsjahr - MITMAS_CFI4
        // Warengruppe - WRG_2
        // Warenhauptgruppe - WHG_2
        // Zielgruppe - Ziel

        // Artikelnummer - articleId
        private string _artikelnummer = artikelnummer;
        // Marke - MRK
        private string _marke = marke;
        // Material 1 - MAT
        private string _material1 = material1;
        // Material 2 - MAT2
        private string _material2 = material2;
        // Material 3 - MAT3
        private string _material3 = material3;
        // Legierung 1 - LEG
        private string _legierung1 = legierung1;
        // Legierung 2 - LEG2
        private string _legierung2 = legierung2;
        // Legierung 3 - LEG3
        private string _legierung3 = legierung3;
        // Kollektion - KOLL
        private string _kollektion = kollektion;
        // Warengruppe - WRG_2
        private string _warengruppe = warengruppe;
        // Warenhauptgruppe - WHG_2
        private string _warenhauptgruppe = warenhauptgruppe;
        // Geschlecht - ZIEL
        private string _geschlecht = geschlecht;
        #endregion

        #region Propeties
        public string Artikelnummer { get => _artikelnummer; set => _artikelnummer = value; }
        public string Marke { get => _marke ; set => _marke = value; }
        public string Material1 { get => _material1 ; set => _material1 = value; }
        public string Material2 { get => _material2 ; set => _material2 = value; }
        public string Material3 { get => _material3 ; set => _material3 = value; }
        public string Legierung1 { get => _legierung1 ; set => _legierung1 = value; }
        public string Legierung2 { get => _legierung2 ; set => _legierung2 = value; }
        public string Legierung3 { get => _legierung3 ; set => _legierung3 = value; }
        public string Kollektion { get => _kollektion ; set => _kollektion = value; }
        public string Warengruppe { get => _warengruppe ; set => _warengruppe = value; }
        public string Warenhauptgruppe { get => _warenhauptgruppe ; set => _warenhauptgruppe = value; }
        public string Geschlecht { get => _geschlecht ; set => _geschlecht = value; }
        #endregion

        #region Contructors
        public Article(string artikelnummer) : this(artikelnummer, "", "", "", "", "", "", "", "", "", "", "")
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
        public string GetArtikelnummer()
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
        public void SetArtikelnummer(string artikelnummer)
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
