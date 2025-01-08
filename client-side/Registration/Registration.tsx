import React from "react"
import { initRegistrationState, RegistrationState } from "./registration.state"
import { Person } from "../Home/home.state"
import { submit } from "./registration.api"

export interface RegistrationProps{
    backToHome :()=>void
    //insertPerson:(person:Person)=>void
}

export class RegistrationForm extends React.Component<RegistrationProps,RegistrationState> {
    constructor(props:RegistrationProps){
        super(props)
        this.state = initRegistrationState
    }

    render():JSX.Element{
        let condition = false
        return(
            <div>
                <div>
                    Welcome to our Registration Page
                </div>
                <div>
                    Name:
                    <input
                        value = {this.state.name}
                        onChange={e=>this.setState(this.state.updateName(e.currentTarget.value))}
                        >
                    </input>
                </div>
                <div>
                    Last Name:
                    <input
                        value = {this.state.lastname}
                        onChange={e=>this.setState(this.state.updateLastName(e.currentTarget.value))}
                        >
                    </input>
                </div>
                <div>
                    Age:
                    <input
                        value = {this.state.age}
                        type = "number"
                        onChange={e=>this.setState(this.state.updateAge(e.currentTarget.valueAsNumber))}
                        >
                    </input>
                </div>
                <div>
                    {
                        condition ? <div>
                            condition is true
                        </div> :
                        null
                    }
                </div>
                <div>
                    <button
                        onClick={e => {
                            //this.props.insertPerson({name: this.state.name,lastname:this.state.lastname,age:this.state.age})
                            submit({name: this.state.name,lastname:this.state.lastname,age:this.state.age})
                            .then(() => this.setState(this.state.clearContents()))
                            }
                        }
                    >
                        Submit
                    </button>
                </div>
                <div>
                    <button
                        onClick={e=> this.props.backToHome()}
                    >
                        Back
                    </button>
                </div>    
                
            </div>
           
        )
    }

}