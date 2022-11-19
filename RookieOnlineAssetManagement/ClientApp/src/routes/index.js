import { Fragment } from "react";
import { Routes, Route, useRoutes } from "react-router-dom";
// layout
import Layout from "../layout";
import MinimalLayout from "../layout/MinimalLayout";
import MainLayout from "../layout/MainLayout";
import Assignment from "../scenes/Home";
import ErrorPage from "../scenes/error";
const Router = () => {
    return useRoutes([
        {
            path: "/",
            element: <MainLayout />,
            children: [
                {
                    path: "/",
                    element: <Assignment />,
                },
            ],
        },
    ]);
};

export default Router;

// ============================
