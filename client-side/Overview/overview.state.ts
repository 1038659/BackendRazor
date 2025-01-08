import { PersonEntry } from "../Home/home.state"

export type OverviewLoader = 

    |{kind: "loading"} 
    |{
        kind: "loaded",
        value: PersonEntry[]
     }
    |{kind: "API ERROR"}  

export interface OverviewState {
    overviewLoader : OverviewLoader
    updateLoader : (overviewLoader: OverviewLoader)=>(state:OverviewState) => OverviewState
 }

export const initOverview:OverviewState = {
    overviewLoader :{kind: "loading"},
    updateLoader : (overviewLoader: OverviewLoader)=>(state:OverviewState): OverviewState => ({
        ...state,
        overviewLoader: overviewLoader
    })
}