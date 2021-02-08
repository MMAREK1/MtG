# MtG
prvotne treba mat nainstalovane .NET SDK (Software Development Kit)
https://download.visualstudio.microsoft.com/download/pr/75483251-b77a-41a9-9ea2-05fb1668e148/2c27ea12ec2c93434c447f4009f2c2d2/dotnet-sdk-5.0.102-win-x64.exe

v Príkazovom riadku sa treba dostat do priecinku kde sa tento projekt nachadza
nasledne treba zadat
dotnet run
to deploy-ne stranku a mozete prezerat karty na  
http://localhost:5000/Karty

vedla tlacidla Filter je dropdown list, ktory urcuje z akeho json suboru karty taha

0VSETKY obsahuje vsetky moje karty bez informacie kolko kusov tych kariet mam
zvysne tri som zrobil aby zobrazovalo aj pocet tak ako som ich dostal , zial FOIL/showcase/borderless informaciu som zatial este nezistil ako sprostredkovat

po kliknuti na Transform a modal_dfc karty sa zobrazi druha strana

filtre pracuju aj s druhou stranou , rarita funguje ako OR, zvysok ako AND
