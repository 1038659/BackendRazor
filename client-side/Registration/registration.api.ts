import { Person } from "../Home/home.state"

export const submit = async (person:Person): Promise<void> => {
    //fetch(route, http request)
    await fetch("api/storage",{
            method: "POST",
            body: JSON.stringify(person),
            headers : {
                "content-type" : "application/json"
            }
    })
}

export const addPerson = (person:Person): Promise<void> => {
  return fetch("api/storage",
        { method: "POST",
          headers : { "content-type" : "application/json" },
          body: JSON.stringify(person)
        }
       )
       .then(_ => undefined)
      }

export const submit2 = (person: Person): Promise<void> =>
    fetch("api/storage", {
      method: "POST",
      body: JSON.stringify(person),//We have to sringify(serialize) the json to store it
      headers: {
        "content-type": "application/json"
      }
    })
    .then(_ => undefined)
  