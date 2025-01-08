
//Moved to Home
// interface Person{
//     name:string,
//     lastname:string,
//     age:number
// }

import { Person } from "../Home/home.state"

export type RegistrationState= Person & {
    //moved to home 
    // storage : Map<number,Person>,
    // id:number,
    updateName:(name: string) => (state:RegistrationState) => RegistrationState
    updateLastName:(name: string) => (state:RegistrationState) => RegistrationState
    updateAge:(name: number) => (state:RegistrationState) => RegistrationState
    clearContents:()=> (state:RegistrationState) => RegistrationState
    //Moved to Home
    //insertPerson:(state:RegistrationState) => RegistrationState
}


export const initRegistrationState: RegistrationState ={
    name: "",
    lastname: "",
    age: 21,
    updateName:(name: string) => (state:RegistrationState) : RegistrationState =>({
       ...state, 
       name:name
    }),
    updateLastName:(name: string) => (state:RegistrationState) : RegistrationState =>({
        ...state, 
        lastname:name
     }),
    updateAge:(age: number) => (state:RegistrationState): RegistrationState =>({
        ...state,
        age:age
    }),
    //Moved to Home
    // insertPerson:(state:RegistrationState): RegistrationState =>({
    //     ...state,
    //     id:state.id+1,
    //     storage : state.storage.set(state.id, {
    //         name:state.name,
    //         lastname:state.lastname,
    //         age :state.age
    //     })
    // })
    clearContents:()=> (state:RegistrationState): RegistrationState =>({
        ...state,
        age: 21,
        name:"",
        lastname:""
    })
}

