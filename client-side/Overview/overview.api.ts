import { PersonEntry } from "../Home/home.state"

export const loadPeople = (): Promise<PersonEntry[]> =>
  fetch("api/storage/all", {
    method: "GET"
  })
  .then(response => response.json())
  .then(content => content as PersonEntry[])

export const OverviewApi = {
  loadPeople: async () : Promise<PersonEntry[]> => {
    return await fetch("api/storage/all",
                        {method: "GET"}
                      )       
                      .then(_ => _.json())
                      .then(__ => __ as PersonEntry[])
  }
}  

// export const deletePerson = async (id: string): Promise<void> => {
//   await fetch(`api/storage?id=${id}`, {
//     method: "DELETE"
//   })
// }

export const deletePerson = async (id: string): Promise<void> => {
    await fetch(`api/storage?id=${id}`, {
        method: "DELETE"
        })}
    //.then(_ =>undefined)