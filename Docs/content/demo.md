---
title: Demo
date: 2020-06-26T09:28:12+01:00
author: griep.marco
layout: _default
permalink: /demo/
---

DSM Management Suite ermöglicht Ihnen mehr Transparenz in Ihrem Patch Management. Mit meiner Demo-Version können Sie ganz einfach die Möglichkeiten der DSM Management Suite ansehen ohne gleich den Server installieren oder Ihre DSM Umgebung anbinden zu müssen. Dies habe ich bereits für Sie erledigt.

#### Inhalt der Demo

Mit der Demo Version haben Sie Zugriff auf eine Test-Umgebung. Benutzername und Passwörter lassen sich nicht ändern und alles was Sie in der Demo Version machen wird regelmäßig zurückgesetzt. Folgende Komponenten stehen Ihnen in der Demo Version zur Verfügung:

* Windows Dashboard
* Web Dashboard
* Möglichkeit mit der PowerShell die API anzusteuern

#### Wie Sie mit der Demo arbeiten
Um die Synchronisation mit Ihrer DSM Umgebung ausführen zu können, brauchen Sie die PSX PowerShell Extensions. In der Demo Version ist diese jedoch nicht notwendig da wir mit Bestandsdaten arbeiten und keine synchroniation durchführen.

##### 1. Laden Sie sich das Windows Dashboard herunter
Die wichtigste Komponente ist das Windows Dashboard. Hier haben Sie die diverse Reports zu erstellen und erhalten einen detaillierten Überblick über Ihr Patch Management. Laden Sie sich hier das aktuelle Setup herunter und installieren Sie die Anwendung.

[Jetzt das Windows Dashboard herunterladen]()

###### Installationsvoraussetzungen
* .Net Framework 4.5
* PowerShell Version 5

**Empfohlen wird ebenfalls:**
* ImportExcel PowerShell Modul von dfinke. [Download](https://github.com/dfinke/ImportExcel)

##### 2. Anwendung einrichten
Starten Sie die Anwendung und geben Sie als API Adresse ein:
***https://api.dsm-management-suite.de/api***

##### 3. Als Benutzer anmelden
Der Benutzername für die Demo-Version ist:

* Benutzer: demo
* Passwort: demo1234

Wenn alles richtig gemacht wurde, sollten Sie sich nun einloggen können und Daten sehen.

##### 4. Die Web Demo ansehen
Um Sie das Web Dashboard anzusehen können Sie ganz einfach auf folgende Adresse gehen und den oben genannten Benutzername und Passwort eingeben:

[Web Dashboard](https://demo.dsm-management-suite.de/)

#### Weitere Artikel

* [Wie Sie mit Python individuelle Reports aus der Web API nutzen](https://www.marcogriep.de/blog/mit-python-automatisiert-patch-reports-aus-dsm-management-suite-erstellen/)
* [Englisch: Wie Sie mit Python individuelle Reports aus der Web API nutzen](https://www.marcogriep.com/blog/automatically-create-patch-reports-from-dsm-management-suite-with-python/)
* [Deutsche Installationsanleitung](https://www.marcogriep.de/blog/dsm-management-suite-anleitung/)
* [Englische Installationsanleitung](https://www.marcogriep.com/blog/dsm-management-suite-manual/)
