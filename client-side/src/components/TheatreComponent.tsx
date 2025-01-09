import React from 'react';
import { Theatre as TheatreType } from '../Types/Theatre'; // Import the Theatre type

interface TheatreProps {
    theatre: TheatreType; // Theatre object passed as a prop
    onDelete: (id: number) => void; // Callback for delete action
    onEdit: (theatre: TheatreType) => void; // Callback for edit action
}

const Theatre: React.FC<TheatreProps> = ({ theatre, onDelete, onEdit }) => {
    return (
        <div className="theatre">
            <h3>{theatre.name}</h3>
            <p>Location: {theatre.price}</p>
            <div className="theatre-actions">
                <button onClick={() => onEdit(theatre)}>Edit</button>
                <button onClick={() => onDelete(theatre.id)}>Delete</button>
            </div>
        </div>
    );
};

export default Theatre;
