# Návod na GitHub a GitKraken


## Co je GitHub

- GitHub nám bude sloužit k snadnějšímu sdílení souborů mezi počítači (školními, domácími i mezi sebou). Když někdo udělá změnu, všichni ji uvidí i u sebe (hodně zjednodušeně řečeno). Už žádné kopírování z Program.cs a podobně.
- Dále umožňuje držet vícero verzí vašeho programu najednou, takže pokud jen experimentujete a nechcete si poškodit, co už máte naprogramováno funkčního, tak se můžete přes Git vždy vrátit k předešlé funkční verzi.
- Navíc umožňuje pohodlně kolaborovat více lidem na jednom větším projektu.
- Jednoduše se pak také váš program nasdílí.
- **Vytvořte si účet (pokud už ho nemáte) na https://github.com/**


## Desktopová aplikace GitKraken 
Aby se nám s GitHubem a funkcemi, které nabízí, dobře pracovalo, doporučuji si stáhnout aplikaci **GitKraken** (https://www.gitkraken.com/download). Existují samozřejmě i další možnosti jako desktopová aplikace od GitHub nebo třeba přístup přes Příkazovou řádku, ale ani jedno není tak vizuálně přehledné. Daň za vzhled spočívá v tom, že v neplacené verzi GitKrakena musíte mít všechno svoje dílo veřejné.

### Práce s repozitáři, *Push* a *Pull*
Vše najdete v horním menu pod *File*.
#### Clone Repo
Sledujte repozitář, který už na GitHubu je. Stáhnete se vám do vašeho složkového systému a pak pouhým kliknutím na ***Pull*** se vám stáhnou aktuální změny. Stačí zadat správnou URL a vybrat si vhodné umístění ve vašem složkovém systému.

**TODO č. 1:** Naklonujte si repozitář https://github.com/Trinithea/GitPrgSem1_24_25.git. Ti, co mi poslali svůj username by v něm měli moct dělat změny.

#### *Push*nutí změn
Když uděláme v repozitáři nějakou úpravu, v GitKrakenovi hned uvidíme, že si jí všiml, a dává nám možnost ji rovnou *push*nout na internet. Je ovšem potřeba ještě:
- *Stage file* - označit soubory, jejichž změny chceme nahrát
- napsat *Commit message* - krátký popisek, co za změnu jsme provedli
- stisknout *Commit Changes ...* 
- a nahoře *Push*nout

**TODO č. 2:** Ve vašem souborovém systému si najděte naklonovaný repozitář a do složky *HOMEWORKS/Markdown* vložte váš Markdown soubor z domácího úkolu. Úspěšně pushněte změny. 

#### Init Repo
Založte si vlastní repozitář, kam si ukládejte vlastní úkoly, projekty. Stačí zadat jméno repozitáře a kam si ho lokálně uložíte (prosím mimo *GitPrgSem1_24_25*). 

Pokud víte, že tam budete ukládat např. projekty z *Visual Studia* nastavte podle toho ***.gitignore***, tím se nebudou zbytečně uploadovat nějaké pomocné soubory.

Na závěr je potřeba provést ještě první *Push*, aby se vám repozitář nahrál na internet. Váš repozitář teď má URL (najdete ji na github.com) a můžete si ho doma *naklonovat*. 

Na github.com můžete u repozitáře nastavit, kdo v něm bude moct pushovat změny přes *Settings -> Collaborators*.

