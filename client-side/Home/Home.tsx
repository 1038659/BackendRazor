import React from "react"
import { HomeState, initHomeState, Person } from "./home.state"
import { RegistrationForm } from "../Registration/Registration"
import { Overview } from "../Overview/Overview"

export class Home extends React.Component<{},HomeState>{
    constructor(props:{}){
        super(props)
        this.state = initHomeState
    }

    render():JSX.Element{
        if(this.state.view == "home"){
            return(
                <div>
                    Welcome to the Home Page
                    <div>
                        <button
                            onClick={e=> this.setState(this.state.updateViewState("registration"))}   
                        >
                            Registration</button>

                        <button
                            onClick={e=> this.setState(this.state.updateViewState("overview"))}  
                        >
                            Overview</button>
                    </div>  
                </div>
            )
        }
        else if (this.state.view == "registration"){
            return (<RegistrationForm 
                backToHome={()=>this.setState(this.state.updateViewState("home"))}
                //insertPerson={(person:Person)=>this.setState(this.state.insertPerson(person))}   

            />)
        }
        else{
            return(
                <Overview
                    //storageToShow = {this.state.storage}
                    backToHome={()=>this.setState(this.state.updateViewState("home"))}
                 />
                )
        } 
    }
}