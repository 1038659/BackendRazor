import React from "react";
import { initOverview, OverviewState } from "./overview.state";
import { loadPeople, OverviewApi, deletePerson } from "./overview.api";


export interface OverviewProps{
    backToHome :()=>void 
    //storageToShow : Map<number,Person>,
}

export class Overview extends React.Component<OverviewProps,OverviewState>{
    constructor(props:OverviewProps){
        super(props) 
        this.state = initOverview
    } 


    loadOverview(){
        if (this.state.overviewLoader.kind == "loading"){
            OverviewApi.loadPeople() 
            .then(people => 
                this.setState(this.state.updateLoader({
                    kind: "loaded",
                    value:people
                }))
            )
            .catch(() => this.setState(this.state.updateLoader({kind: "API ERROR"})))
        }
    }

    reloadOverview(){
            OverviewApi.loadPeople() 
            .then(people => 
                this.setState(this.state.updateLoader({
                    kind: "loaded",
                    value:people
                }))
            )
            .catch(() => this.setState(this.state.updateLoader({kind: "API ERROR"})))
    }
    
    //lifecycle method of react
    componentDidMount(): void {
        this.loadOverview()
    }

    render():JSX.Element{
        return(
            <div>
                Welcome to overview Page !!
                <div>
                    <button
                        onClick={e=> this.props.backToHome()}
                    >
                        Back
                    </button>
                </div>
                {   
                    this.state.overviewLoader.kind == "loading" ? 
                        <div>
                            Loading ...
                        </div> 
                        : 
                        this.state.overviewLoader.kind == "API ERROR" ?
                        <div>API ERROR</div>
                            :
                            this.state.overviewLoader.value.map(
                            person => (
                                <div key={person.id}>
                                    Name: {person.name}
                                    LastName: {person.lastname}
                                    Age: {person.age}
                                    Id: {person.id}
                                    <button onClick={() => 
                                    {   //chain of call-backs: 
                                        //updateLoader({kind: "loading"}) -> deletePerson(person.id) -> this.reloadOverview()
                                        this.setState(this.state.updateLoader({kind: "loading"}), () => { 
                                                                                deletePerson(person.id)
                                                                                .then(_ => this.reloadOverview())
                                                                               } 
                                                     )
                                                              
                                     }
                                     //Other chain of call-backs:
                                    /*{    
                                              deletePerson(person.id)
                                              .then(_ =>                          
                                                    this.setState(this.state.updateLoader({kind: "loading"}), 
                                                                    () => {
                                                                        this.reloadOverview()
                                                                        } )
                                               )                          
                                           }*/
                                        }> 
                                        Delete 
                                    </button>
                                </div>
                                
                            )
                        )
                }
            </div>
        )
    }
}

