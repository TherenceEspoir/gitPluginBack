
# [GitPlugin]

## Equipe

- [HOUEDJI , Espoir, therencecossi@gmail.com]

## Présentation du projet

Ce projet est une application web permettant de construire un profil synthèse rapide et simple d'un compte GitLab. L'application utilise Dash pour le frontend et ASP.NET Core pour le backend. L'objectif est de fournir une interface utilisateur pour visualiser les projets, les issues, les merge requests et d'autres informations liées aux comptes GitLab.

## Technologies utilisées

### Frontend
- **Dash**: Utilisé pour construire l'interface utilisateur. Dash est choisi car il permet de créer des applications web interactives sans avoir besoin de maîtriser une technologie frontend complexe comme React ou Angular.
  - **Avantages**: 
    - Simplicité d'utilisation
    - Intégration facile avec Python pour les calculs statistiques et les graphiques
  - **Inconvénients**:
    - Performance parfois lente
    - Prise en main des redirections et gestion des états complexes
  
### Backend
- **ASP.NET Core**: Utilisé pour construire l'API RESTful qui interagit avec l'API GitLab.
- **C#**: Langage de programmation utilisé pour développer le backend.
- **DotNetEnv**: Utilisé pour gérer les variables d'environnement.
  
### Base de données
- Aucune base de données n'est utilisée pour ce projet. Toutes les données sont récupérées dynamiquement depuis l'API GitLab.

## Gestion de projet

- **Git**: Utilisé pour le contrôle de version.
- **GitLab**: Dépôt de code 

## Expérience générale

- **Niveau de l'équipe**: Intermédiaire en C# et complètement débutant en Dash.
- **Apprentissages**: Familiarisation avec Dash, intégration d'API RESTful en ASP.NET Core.
- **Technologies futures**: Possibilité d'explorer d'autres frameworks frontend pour améliorer les performances.

## Installation

### Prérequis

- **Python 3.8**: Le projet utilise Python 3.8.0 pour le frontend.
- **.NET 8.0**: Utilisé pour le backend.

### Instructions d'installation

1. **Cloner le dépôt du frontend**:
   ```bash
   git clone https://gitlab.dpt-info.univ-littoral.fr/houedji.espoir/gitpluginfrontend
   cd gitpluginfrontend `


2. **Cloner le dépôt du backend**:
   ```bash
   git clone https://gitlab.dpt-info.univ-littoral.fr/houedji.espoir/gitplugin.git
   cd gitplugin `

5. ****Executer le projet backend**:
   ```bash
   cd gitplugin
   cd GitPluginAPI
   dotnet run

3. **Configurer l'environnement virtuel utilisé pour le frontend**:
   ```bash
   cd gitpluginfrontend
   python3.8 -m venv env
   source env/bin/activate 
   pip install -r requirements.txt 
   python app.py `
   
4. ****Configurer les variables d'environnement pour le backend , fichier env et son contenu**:
   ```bash
   cd gitplugin
   touch .env
   GITLAB_PAT=votre_token_gitlab 
  
### Authentification

L'application utilise un système d'authentification basique avec JWT. Utilisez les informations suivantes pour vous connecter :

-   **Email**: betco@betco.com
-   **Mot de passe**: toto

## Routes d'API


### Projet

-   `[GET] /api/Project`: Récupérer tous les projets.
-   `[GET] /api/Project/{projectId}`: Récupérer un projet par ID.
-   `[POST] /api/Project`: Créer un nouveau projet.
-   `[PUT] /api/Project/{projectId}`: Mettre à jour un projet existant.
-   `[DELETE] /api/Project/{projectId}`: Supprimer un projet.

### Issue

-   `[GET] /api/Issue/{projectId}/issues`: Récupérer toutes les issues d'un projet.
-   `[GET] /api/Issue/{projectId}/issues/{issueId}`: Récupérer une issue par ID.
-   `[POST] /api/Issue/{projectId}/issues`: Créer une nouvelle issue.
-   `[PUT] /api/Issue/{projectId}/issues/{issueId}`: Mettre à jour une issue.
-   `[DELETE] /api/Issue/{projectId}/issues/{issueId}`: Supprimer une issue.

### Merge Request

-   `[GET] /api/MergeRequest/{projectId}/merge_request`: Récupérer toutes les merge requests d'un projet.
-   `[POST] /api/MergeRequest/{projectId}/merge_request`: Créer une nouvelle merge request.
-   `[PUT] /api/MergeRequest/{projectId}/merge_request/{mergeRequestId}`: Mettre à jour une merge request.
-   `[DELETE] /api/MergeRequest/{projectId}/merge_request/{mergeRequestId}`: Supprimer une merge request.
-   `[GET] /api/MergeRequest/{projectId}/merge_request/{mergeRequestId}`: Récupérer une merge request par ID.


![Description de l'image](https://gitlab.dpt-info.univ-littoral.fr/houedji.espoir/gitplugin/-/raw/main/images/capture1.png)


## Notes finales

-   La documentation de l'API est disponible via Swagger à l'adresse `http://localhost:5299/swagger/index.html`.
-   Les décisions de conception, les choix technologiques et les défis rencontrés ont été documentés tout au long du développement.
-   Étant donné que je viens de commencer sur Dash, j'ai eu pas mal de difficultés, surtout au niveau de la redirection et du callback, donc à certains endroits, il est important de réactualiser la page après un login.
-   Pour toute question ou suggestion, veuillez contacter l'équipe à l'adresse mail fournie.

