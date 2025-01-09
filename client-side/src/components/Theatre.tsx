import React from 'react';

interface ProductProps {
    id: number;
    name: string;
    price: number;
    description: string;
}

const Product: React.FC<ProductProps> = ({ id, name, price, description }) => {
    return (
        <div className="product">
            <h2>{name}</h2>
            <p>{description}</p>
            <p>Price: ${price.toFixed(2)}</p>
        </div>
    );
};

export default Product;