import axios from 'axios';
import { Theatre } from '../Types/Theatre';

const api = axios.create({
    baseURL: 'http://localhost:5000/api/Theatres',
});

export const getTheatres = async () => {
    const response = await api.get<Theatre[]>('/');
    return response.data;
};

export const getTheatre = async (id: number) => {
    const response = await api.get<Theatre>(`/${id}`);
    return response.data;
};

export const createTheatre = async (Theatre: Omit<Theatre, 'id'>) => {
    const response = await api.post<Theatre>('/', Theatre);
    return response.data;
};

export const updateTheatre = async (id: number, Theatre: Theatre) => {
    await api.put(`/${id}`, Theatre);
};

export const deleteTheatre = async (id: number) => {
    await api.delete(`/${id}`);
};


