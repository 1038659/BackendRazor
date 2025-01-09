import React, { useEffect, useState } from 'react';
import { getTheatres, deleteTheatre } from '../api/TheatreApi';
import { Theatre } from '../Types/Theatre';

const TheatresList: React.FC = () => {
    const [theatres, setTheatres] = useState<Theatre[]>([]);

    useEffect(() => {
        fetchTheatres();
    }, []);

    const fetchTheatres = async () => {
        const data = await getTheatres();
        setTheatres(data);
    };

    const handleDelete = async (id: number) => {
        await deleteTheatre(id);
        fetchTheatres();
    };

    return (
        <div>
            <h1>Theatres</h1>
            <ul>
                {theatres.map((theatre) => (
                    <li key={theatre.id}>
                        {theatre.name} - ${theatre.price.toFixed(2)}
                        <button onClick={() => handleDelete(theatre.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default TheatresList;
