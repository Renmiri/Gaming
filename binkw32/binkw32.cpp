// binkw32.cpp : Definiert die exportierten Funktionen für die DLL-Anwendung.
//

#include "stdafx.h"
#include "binkw32.h"


// Dies ist das Beispiel einer exportierten Variable.
BINKW32_API int nbinkw32=0;

// Dies ist das Beispiel einer exportierten Funktion.
BINKW32_API int fnbinkw32(void)
{
	return 42;
}

// Dies ist der Konstruktor einer Klasse, die exportiert wurde.
// Siehe binkw32.h für die Klassendefinition.
Cbinkw32::Cbinkw32()
{
	return;
}
