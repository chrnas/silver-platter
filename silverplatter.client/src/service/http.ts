const API_BASE = "http://localhost:5218/api";

async function http<T>(url: string, options?: RequestInit): Promise<T> {
    const response = await fetch(`${API_BASE}/${url}`, {
        headers: {
            "Content-Type": "application/json",
        },
        ...options,
    }); // Might need changes to the url or formatting depending on how we send

    if (!response.ok) {
        const message = await response.text();
        throw new Error(message || "API error");
    }

    return response.json(); 
}

export default http;