using System;
using System.Net.Http;
using System.Net.Http.Json;

// TODO: As a top-level statements exercise, use only this single file to define everything!

if (args.Length != 1)
{
    // TODO: define local PrintHelp function to print help and exit
}

// TODO: utilize https://swapi.dev/ to search for a person with a given name
//       Hint: Use https://swapi.dev/documentation and look for "Searching"
using HttpClient client = new HttpClient();
var requestUri = "?";
var response = await client.GetFromJsonAsync<PersonsDTO>(requestUri);

if (response?.Count != 1)
{
    Console.WriteLine("There is no single answer to your question!");
}
else
{
    // TODO: 
    Console.WriteLine($"{person.Name} was born {person.Birth_Year}.");
}

// TODO: define PersonDTO and PersonsDTO records to deserialize (only necessary)
//       fields from https://swapi.dev/api/people/?search=... results.

