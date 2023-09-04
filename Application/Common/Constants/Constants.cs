using System.Collections.Generic;

namespace Immowert4You.Application.Common.Constants
{
    public static class Constants
    {
        public static List<ImpovmentGroup> GetImprovments()
        {
            return new List<ImpovmentGroup>
            {
                new ImpovmentGroup
                {
                    Header = "Aufräumen",
                    Improvments = new List<Improvment>
                    {
                        new Improvment
                        {
                            IsHeader = true,
                            Title= "Eingangsbereich",
                        },
                        new Improvment
                        {
                            Title = "Schuhe wegräumen"
                        },
                        new Improvment
                        {
                            Title = "Post wegräumen"
                        },
                        new Improvment
                        {
                            Title = "Garderobe max. 3 Kleidungsstücke"
                        },
                        new Improvment
                        {
                            IsHeader = true,
                            Title="Küche / Essbereich"
                        },
                        new Improvment
                        {
                            Title = "Esstisch abräumen"
                        },
                        new Improvment
                        {
                            Title = "Arbeitsfläche leerräumen"
                        },
                        new Improvment
                        {
                            Title = "Putz- Utensilien entfernen"
                        },
                        new Improvment
                        {
                            Title = "Kühlschranktür von persönlichen"
                        },
                        new Improvment
                        {
                            Title = "Dingen befreien"
                        },
                        new Improvment
                        {
                            Title = "Mülleimer ausleeren"
                        },
                        new Improvment
                        {
                            IsHeader = true,
                            Title = "Badezimmer / WC"
                        },
                        new Improvment
                        {
                            Title = "Utensilien vom Spülbecken entfernen"
                        },
                        new Improvment
                        {
                            Title = "Frische Hand- und Badetücher aufhängen"
                        },
                        new Improvment
                        {
                            Title = "Bademantel entfernen"
                        },
                        new Improvment
                        {
                            Title = "Toilettendeckel schließen"
                        },
                        new Improvment
                        {
                            IsHeader = true,
                            Title = "Innenbereich Allgemein"
                        },
                        new Improvment
                        {
                            Title = "Entfernen Sie alle persönlichen Dinge wie Fotos, Postkarten, Pokale, Urkunden, Trophäen, Jagdgeweihe, usw."
                        },
                        new Improvment
                        {
                            Title = "Aschenbecher entfernen"
                        },
                        new Improvment
                        {
                            Title = "Beseitigen Sie Stolperfallen"
                        },
                        new Improvment
                        {
                            Title = "Haustier – Equipment entfernen"
                        },
                        new Improvment
                        {
                            Title = "Kleidungsstücke entfernen"
                        }
                    },
                    Slogan = "Der Reinigungsprofi in Ihrem Bezirk"
                },
                new ImpovmentGroup
                {
                    Header = "Sauberkeit",
                    Improvments = new List<Improvment>
                    {
                        new Improvment
                        {
                            Title = "Eingangsbereich",
                            IsHeader = true,
                        },
                        new Improvment
                        {
                            Title = "Fußmatte säubern"
                        },
                        new Improvment
                        {
                            Title = "Küche / Essbereich",
                            IsHeader = true
                        },
                        new Improvment
                        {
                            Title = "Arbeitsfläche säubern"
                        },
                        new Improvment
                        {
                            Title = "Spüle säubern und polieren"
                        },
                        new Improvment
                        {
                            Title = "Herd säubern"
                        },
                        new Improvment
                        {
                            Title = "Esstisch und Stühle säubern"
                        },
                        new Improvment
                        {
                            Title = "Vitrinen säubern"
                        },
                        new Improvment
                        {
                            Title = "Badezimmer / WC",
                            IsHeader = true
                        },
                        new Improvment
                        {
                            Title = "Toilette säubern"
                        },
                        new Improvment
                        {
                            Title = "Spülbecken säubern"
                        },
                        new Improvment
                        {
                            Title = "Wasserhähne und Duschköpfe polieren"
                        },
                        new Improvment
                        {
                            Title = "Spiegel polieren"
                        },
                        new Improvment
                        {
                            Title = "Innenbereich Allgemein",
                            IsHeader = true
                        },
                        new Improvment
                        {
                            Title = "Böden saugen und wischen"
                        },
                        new Improvment
                        {
                            Title = "Fenster putzen"
                        },
                        new Improvment
                        {
                            Title = "Fensterbänke säubern"
                        },
                        new Improvment
                        {
                            Title = "Heizkörper säubern"
                        },
                        new Improvment
                        {
                            Title = "Alle Kästen und Schränke säubern"
                        },
                    },
                    Slogan = "Der Reinigungsprofi in Ihrem Bezirk"
                },
                new ImpovmentGroup
                {
                    Header = "Homestaging",
                    Header2 = "Was ist Homestaging?",
                    Paragraph = "Homestaging ist die optimale Präsentation einer Immobilie für den Verkauf. Die Immobilie wird „neutralisiert“, das heißt Der persönliche Touch des Vorbesitzers wird entfernt. Beim Homestaging werden Möbel, Farbe, Licht, Wand -und Fußbodengestaltung optimal zum Einsatz gebracht.",
                    Slogan = "Um das Optimum aus Ihrer Immobilie zu holen"
                },
                new ImpovmentGroup
                {
                    Header = "Diverse Reparaturen",
                    Improvments = new List<Improvment>
                    {
                        new Improvment
                        {
                            Title = "Alle Lampen funktionstüchtig",
                        },
                        new Improvment
                        {
                            Title = "Fenster sind gut eingestellt und geschmiert",
                        },
                        new Improvment
                        {
                            Title = "Türen schließen gut und quietschen nicht",
                        },
                        new Improvment
                        {
                            Title = "Schalter und Steckdosen sind im gutem Zustand",
                        },
                    },
                    Slogan = "Um das Optimum aus Ihrer Immobilie zu holen"
                },
                new ImpovmentGroup
                {
                    Header = "Außenbereich",
                    Improvments = new List<Improvment>
                    {
                        new Improvment
                        {
                            Title = "Rasen mähen"
                        },
                        new Improvment
                        {
                            Title = "Hecke schneiden"
                        },
                        new Improvment
                        {
                            Title = "Blumenbeete aufhübschen"
                        },
                        new Improvment
                        {
                            Title = "Pool säubern"
                        },
                        new Improvment
                        {
                            Title = "Pflastersteine und Fließen mit einem Hochdruckreiniger säubern"
                        },
                        new Improvment
                        {
                            Title = "Fenster, Haustür und Terrassentür säubern"
                        },
                        new Improvment
                        {
                            Title = "Gartenmöbel säubern"
                        },
                    },
                    Slogan = "Ihr Profi für den Außenbereich"
                },
                new ImpovmentGroup
                {
                    Header = "Immobilienmakler",
                    Paragraph = "Mit einem regionalen Immobilienmakler können Sie den optimalen Preis für Ihre Immobilie erzielen. Dieser kennt die Gegend und weiß wo die Preise fallen, stagnieren oder steigen. Der Immobilienmakler klärt sie auch über den Verkaufsablauf, den Energieausweis, die Immobilienertragssteuer, Ihrer Rechtssicherheit und eventuellen wertsteigenden Eigenschaften auf."
                }
            };
        }
    }

    public class ImpovmentGroup
    {
        public string Header { get; set; }
        public string Header2 { get; set; }
        public string Paragraph { get; set; }
        public List<Improvment> Improvments { get; set; }
        public string Slogan { get; set; }
    }

    public class Improvment
    {
        public bool IsHeader { get; set; }
        public string Title { get; set; }
        public bool Value { get; set; }
    }
}
