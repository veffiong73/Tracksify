import type { Metadata } from "next";
import { Work_Sans } from "next/font/google";
import localFont from "next/font/local";

import "./globals.css";

const work_sans = Work_Sans({ subsets: ["latin"] });

const product_sans = localFont({
  src: [
    {
      path: "../public/fonts/product_sans_font/ProductSans-Bold.ttf",
      weight: "700",
      style: "bold",
    },
  ],

  variable: "--font-product-sans",
});

export const metadata: Metadata = {
  title: "Tracksify",
  description:
    "Tracksify is a productivity app that helps you keep track of your daily tasks and goals.",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en" className={`${product_sans.variable}`}>
      <body className={work_sans.className}>{children}</body>
    </html>
  );
}
