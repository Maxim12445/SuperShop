import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Login from './pages/Login';
import Clientes from './pages/Clientes';
import NovoCliente from './pages/NovoCliente';

export default function AppRoutes() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/clientes" element={<Clientes />} />
                <Route path="/cliente/novo/:clienteId" element={<NovoCliente />} />
            </Routes>
        </BrowserRouter>
    );
}