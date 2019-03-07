# Movie Reviews for login user

- ASP.NET MVC application to allow the user to maintain a database of movie reviews for login user 
- Using SQL Local DB and Bootstrap, validate for every single input 
- you can input movie review and update it or delete.

# Code
- Controller/MovieController.cs :  Main Controller for this app
- Models/Domain/Movie.cs :  Movie class (MovieNAme, Rating, Category)
- Models/ViewModels/CreateMovieViewModel.cs : Class for editing Movie review 
- Models/ViewModels/IndexMovieViewModel.cs : Class for creating Movie review 
- Views/Movie/Create.cshtml : HTML for Creating movie review 
- Views/Movie/Edit.cshtml : HTML for editing movie review 
- Views/Movie/index.cshtml : HTML for List of movie review 
- Views/Shared/_Layout.cshtml : HTML for all layout 


# Requirement 
- The rating should be displayed as radio buttons from 1-5.
- The categories should be displayed in a select element. Categories can include: Drama, Comedy, Horror, Romance, Sci-Fi, Adventure
- Movie names should be unique!
- Display the list of current movies that the user has registered.
- Allow the user to edit movies.
- Allow the user to delete movies.

