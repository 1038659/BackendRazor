export type ViewState = "home" | "registration" | "overview"

export interface Person{
    name:string,
    lastname:string,
    age:number
}

export type PersonEntry = 
    Person & 
    {
        id:string
    }

export interface HomeState {
    //storage : Map<number,Person>, //This is a datatype lie a dictionary
    //id:number,
    view:ViewState,
    updateViewState : (view:ViewState) => (state:HomeState)=>HomeState
    //insertPerson:(person:Person)=> (state:HomeState)=>HomeState 
}

export const initHomeState :HomeState ={
    //storage : new Map,
    //id:0,
    view: "home",
    updateViewState : (view:ViewState) => (state:HomeState):HomeState =>({
        ...state,
        view:view
    }),
    // insertPerson:(person:Person)=> (state:HomeState):HomeState =>({
    //     ...state,
    //     id:state.id+1,
    //     storage : state.storage.set(state.id, {
    //         name:person.name,
    //         lastname:person.lastname,
    //         age :person.age
    //     })
    // }),

}