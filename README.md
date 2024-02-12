# Favors and Fates

This is an app to facilitate game playing between partners when sharing presents.

The general idea is, you want to give a present to your partner--some clothes you've bought for them, some art you've made, anything really--but you want to add some extra engagement and excitement by making a game out of it.

In order to use Favors and Fates to facilitate this experience, you define a "favor," with its requested "tokens," along with a corresponding "fate," in a .json file located in C:/FavorsAndFatesApp/FavorRequests. You then run the app locally, on some communal machine your partner has access to, and they will have access to the requested "tokens," which they will need to acquire out in the world. Here, a "token" refers to "any concept in the context of the game being played, to be provided as an image file." Once your partner has collected all their "tokens," they submit them, and receive their "fate," a text string, which can be another clue to a continuation of the game, a password to a .zip file, the location of their present in the world, or really anything.

A default, example .json file will be created the first time the app runs.

The app will display the contents of the .json file with the highest numerical value, e.g. it will display the contents of "9.json" and not "2.json", if it finds both those files.

The app is composed of a React frontend that runs on :3000 and a .NET Core API that runs on :5179. CORs is currently enabled and unrestricted, so that localhost:3000 can make requests to localhost:5179. Many improvements are needed for a more streamlined experience. The file "run.ps1" will launch the app.