import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const Login = ({ setUserData }) => {
    const [formData, setFormData] = useState({
        email: "",
        password: "",
    });

    const [errorMessage, setErrorMessage] = useState("");
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        try {
            const response = await axios.post("http://localhost:5271/api/user/login", formData);
            const { token, tenantId, userId } = response.data;
            
            localStorage.setItem("token", token);
            localStorage.setItem("tenantId", tenantId);
            
            setUserData({ token, tenantId, userId });
    
            navigate("/dashboard");
        } catch (error) {
            setErrorMessage(
                error.response?.data?.message || "Login failed. Please check your credentials and try again."
            );
        }
    };

    // Redirect to the registration page
    const handleRegisterRedirect = () => {
        navigate("/register"); // Adjust the path to match your registration route
    };
    
    return (
        <div className="flex items-center justify-center min-h-screen bg-neutral-900 text-neutral-50">
            <div className="w-full max-w-md p-8 bg-neutral-800 rounded shadow-md">
                <h2 className="text-2xl font-bold mb-6 text-center">Login</h2>
                {errorMessage && <p className="text-red-500 text-center mb-4">{errorMessage}</p>}
                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label className="block text-sm font-medium mb-1">Email:</label>
                        <input
                            type="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                            className="w-full p-2 border border-neutral-700 rounded bg-neutral-900 text-neutral-50"
                        />
                    </div>
                    <div>
                        <label className="block text-sm font-medium mb-1">Password:</label>
                        <input
                            type="password"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                            required
                            className="w-full p-2 border border-neutral-700 rounded bg-neutral-900 text-neutral-50"
                        />
                    </div>
                    <button
                        type="submit"
                        className="w-full py-2 mt-4 bg-blue-500 rounded text-white font-semibold hover:bg-blue-600 transition-colors"
                    >
                        Login
                    </button>
                </form>
                <button
                    onClick={handleRegisterRedirect}
                    className="w-full py-2 mt-4 bg-gray-500 rounded text-white font-semibold hover:bg-gray-600 transition-colors"
                >
                    Register
                </button>
            </div>
        </div>
    );
};

export default Login;
