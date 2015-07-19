// Folgender ifdef-Block ist die Standardmethode zum Erstellen von Makros, die das Exportieren 
// aus einer DLL vereinfachen. Alle Dateien in dieser DLL werden mit dem BINKW32_EXPORTS-Symbol
// (in der Befehlszeile definiert) kompiliert. Dieses Symbol darf für kein Projekt definiert werden,
// das diese DLL verwendet. Alle anderen Projekte, deren Quelldateien diese Datei beinhalten, erkennen 
// BINKW32_API-Funktionen als aus einer DLL importiert, während die DLL
// mit diesem Makro definierte Symbole als exportiert ansieht.
#ifdef BINKW32_EXPORTS
#define BINKW32_API __declspec(dllexport)
#else
#define BINKW32_API __declspec(dllimport)
#endif

// Diese Klasse wird aus binkw32.dll exportiert.
class BINKW32_API Cbinkw32 {
public:
	Cbinkw32(void);
	// TODO: Hier die Methoden hinzufügen.
};

extern BINKW32_API int nbinkw32;

BINKW32_API int fnbinkw32(void);
