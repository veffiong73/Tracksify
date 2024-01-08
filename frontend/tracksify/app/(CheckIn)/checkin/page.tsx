"use client";
import Link from "next/link";
import Logo from "@/components/logo";

import React, { useState } from "react";

const page = () => {
  return (
    <div className="">
      <main className="">
        <div className="  flex  items-center p-4 bg-white">
          <div className="p-4  ">
            <Logo />
          </div>

          <div className="space-x-20 pl-80">
            <a
              href="./app/page.tsx"
              className=" bg-color_hover rounded-full px-2 py-2 m-2 hover:text-text_secondary font-sm hover:text-black text-text_secondary"
            >
              Home
            </a>
            <a href="#" className="text-text_tertiary  font-sm">
              Employee
            </a>
            <a href="#" className="text-text_tertiary font-sm">
              Project
            </a>
            <a
              href="#"
              className=" border rounded-full  bg-text_secondary  text-white  font-sm px-3 py-2 hover:bg-color_hover hover:text-text_tertiary"
            >
              {" "}
              FO
            </a>
          </div>
        </div>
        <div className="p-10 pl-20">
          <h1 className="text-text_tertiary font-bold  text-lg  pt-10 pl-6 mt-4">
            Check In
          </h1>
        </div>
      </main>
    </div>
  );
};

export default page;
