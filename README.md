# Stellar Stingers
Videogioco Arcade 2D realizzato in Unity.

Progetto d'esame di User Experience e Grafica 2D.

L’astronave Perseus-23, comandata dal giocatore (o dai due giocatori) deve sopravvivere schivando gli asteroidi e gli attacchi delle varie meduse aliene, per poi giungere alla temibile Infestatrix Mortalis, la più potente tra le pelagie, più difficile da uccidere, che sparerà tre colpi contemporaneamente tentando di annientare il giocatore.

Una volta uccisa, guadagnerai 10 punti e aumenta la velocità, aumentando di conseguenza la difficoltà di gioco.

Alla sconfitta dell’astronave ti verrà mostrato il punteggio record.

## Comandi
- Frecce direzionali sinistra e destra: Movimento laterale
- Frecce direzionali su e giù: Velocità
- A: Proiettile rosso
- S: Proiettile verde
- D: Proiettile giallo

## Azzerare High Score
Su Windows, per cancellare High Score della Build.

https://virtual-reality-piano.medium.com/solved-where-is-unitys-playerprefs-stored-on-windows-d61d504585e9

- Cliccare tasto Windows.
- Cercare "regedit" e premere invio.
- Cercare HKEY_CURRENT_USER\SOFTWARE\Stellar Stingers Studio\Stellar Stingers
- Aprire la voce High Score e impostare 0.

Per cancellare High Score dell'editor di Unity:
- Nella barra del titolo, selezionare Tools > BGTools > PlayerPrefsEditor
- Impostare highscore a 0

In entrambe le opzioni, non è necessario azzerare score poiché viene azzerato automaticamente all'avvio della partita.
