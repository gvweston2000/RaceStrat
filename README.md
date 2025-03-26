# RaceStratAPI C#/.NET

This API provides functionality for managing and calculating race strategies, including fuel consumption, pit stops, and vehicle information for racing events. The API allows users to interact with vehicle and race data, perform calculations, and manage race strategies for different vehicles and tracks.

## Features

- Manage vehicle details (engine size, fuel efficiency, weight, etc.).
- Calculate fuel consumption per lap and total fuel needed for a race.
- Predict the number of pit stops required based on the vehicle's fuel tank capacity and race conditions.
- Handle various race data, including track length, total laps, and race conditions.

## Formulas

### 1. **Fuel Per Lap Calculation**

The formula for calculating fuel per lap is:

<!-- $$
\text{Fuel Per Lap} = \left( \frac{\text{Track Length}}{\text{Fuel Efficiency}} \right) \times \left( 1 + \frac{\text{Vehicle Weight}}{1000} \times 0.1 \right)
$$ --> 

<div align="center"><img style="background: white;" src="svg/FaeP1HdRgF.svg"></div>

Where:
- **Track Length** is the length of the track in metres (or any other unit as per the system).
- **Fuel Efficiency** is the distance that can be covered per unit of fuel (e.g., km per litre or miles per gallon).
- **Vehicle Weight** is the weight of the vehicle in kilograms (optional, used for a modifier).

### 2. **Total Fuel Needed for the Race**

The total fuel needed for the race can be calculated by multiplying the fuel per lap by the total number of laps:

<!-- $$
\text{Total Fuel Needed} = \text{Fuel Per Lap} \times \text{Total Laps}
$$ --> 

<div align="center"><img style="background: white;" src="svg/ezG3aOVmst.svg"></div>

Where:
- **Fuel Per Lap** is calculated from the previous equation.
- **Total Laps** is the total number of laps for the race.

### 3. **Number of Pit Stops Required**

To calculate the number of pit stops required, the total fuel needed is divided by the fuel tank capacity, and the result is rounded up to the nearest whole number:

<!-- $$
\text{Pit Stops Required} = \lceil \frac{\text{Total Fuel Needed}}{\text{Fuel Tank Capacity}} \rceil
$$ --> 

<div align="center"><img style="background: white;" src="svg/PgU2E8n8hC.svg"></div>

Where:
- **Total Fuel Needed** is calculated from the previous equation.
- **Fuel Tank Capacity** is the maximum fuel a vehicle can hold.

### 4. **Adjust Fuel Based on Speed and Race Conditions**

To account for race conditions, the fuel per lap is adjusted based on average speed and a race condition factor:

<!-- $$
\text{Adjusted Fuel Per Lap} = \text{Fuel Per Lap} \times \left( 1 + \frac{\text{Average Speed}}{200} \times \text{Race Condition Factor} \right)
$$ --> 

<div align="center"><img style="background: white;" src="svg/SEISgBCLMU.svg"></div>

Where:
- **Average Speed** is the average speed of the vehicle during the race (in km/h or miles/h).
- **Race Condition Factor** is a factor that modifies fuel consumption based on conditions such as weather or track characteristics.

### 5. **Fuel and Pit Stop Calculation (Combined)**

Finally, everything is combined into one calculation:

<!-- $$
\text{Total Fuel Needed}, \text{Pit Stops Required} = \text{Fuel And Pit Stop Calculation}( \text{Fuel Efficiency}, \text{Track Length}, \text{Total Laps}, \text{Fuel Tank Capacity}, \text{Vehicle Weight}, \text{Average Speed}, \text{Race Condition Factor})
$$ --> 

<div align="center"><img style="background: white;" src="svg/fTamfpXfAb.svg"></div>

This encapsulates:
1. Calculating fuel per lap.
2. Adjusting for speed and race conditions.
3. Calculating the total fuel needed for the race.
4. Predicting the number of pit stops based on the total fuel required and the fuel tank capacity.

## Technologies Used

- **ASP.NET Core 9**: The framework for building the RESTful API.
- **Entity Framework Core**: For ORM (Object-Relational Mapping) to interact with a database.
- **In-memory Database**: For testing purposes (using InMemoryDatabase for unit tests).
- **xUnit**: For unit testing the service layer and controller logic.

## Requirements

- .NET 9 SDK or higher
- A code editor like Visual Studio or Visual Studio Code
- Postman (optional, for testing API endpoints)

## Installation

### Clone the Repository

```bash
git clone https://github.com/your-username/race-strategy-api.git
cd race-strategy-api
```

### Install Dependencies

Make sure you have the .NET 9 SDK installed. Run the following command to restore the project dependencies:

```
dotnet restore
```

### Build the Project

To build the project, run:

```
dotnet build
```

### Run the Application

You can run the API locally using:

```
dotnet run
```

By default, the API will be accessible at https://localhost:5001 or http://localhost:5000 (depending on your configuration).

### Database Setup

The application uses an in-memory database for testing and development. For production environments, you may want to set up a persistent database like SQL Server or PostgreSQL. You can modify the `appsettings.json` to change the database provider.

To migrate the database (if you switch to a persistent database), use the following command:

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## API Endpoints

### Vehicle Endpoints

GET `/api/vehicles`

Description: Retrieve a list of all vehicles.

Response: A list of vehicle objects.

GET `/api/vehicles/{id}`

Description: Retrieve details of a specific vehicle by its ID.

Response: The vehicle object with the provided ID.

POST `/api/vehicles`

Description: Create a new vehicle.

Request Body: A Vehicle object with details such as name, engine size, fuel efficiency, etc.

Response: The created Vehicle object.

PUT `/api/vehicles/{id}`

Description: Update an existing vehicle.

Request Body: A Vehicle object with updated details.

Response: No content (204 status) if successful.

DELETE `/api/vehicles/{id}`

Description: Delete a vehicle.

Response: No content (204 status) if successful.

### Race Endpoints

GET `/api/races`

Description: Retrieve a list of all races.

Response: A list of race objects, including related vehicle data.

GET `/api/races/{id}`

Description: Retrieve details of a specific race by its ID.

Response: The race object with the provided ID, including related vehicle data.

POST `/api/races`

Description: Create a new race and calculate fuel/pit stops.

Request Body: A Race object with details like track length, total laps, race conditions, and associated vehicle ID.

Response: The created Race object, including calculated fuel per lap, total fuel needed, and pit stops required.

PUT `/api/races/{id}`

Description: Update an existing race and recalculate fuel/pit stops.

Request Body: A Race object with updated details.

Response: No content (204 status) if successful.

DELETE `/api/races/{id}`

Description: Delete a race.

Response: No content (204 status) if successful.

## Running Unit Tests

To run unit tests, use the following command:

```
dotnet test
```

This will run the tests in the RaceStratAPI.Tests project, which includes tests for fuel calculations, pit stop predictions, and error handling.

## License

This project is open-source and available under the MIT License.
