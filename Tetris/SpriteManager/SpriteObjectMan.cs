using System;

namespace Tetris
{
    static class SOM  // SpriteObjecManager i.e. SOM
    {
        static public void drawWelcomeScreen()
        {
            // TODO : edit this and make it pretty! 

            SpriteFont Title = new SpriteFont("T E T R I S ", 307, 550);
            SpriteFont Begin1 = new SpriteFont("  Press the G key", 260, 200);
            SpriteFont Begin2 = new SpriteFont("      to begin!", 260, 180);

            Title.Draw();
            Begin1.Draw();
            Begin2.Draw();

        }

        static public void drawGameOverScreen(GameStats stats)
        {
            int levels = stats.getLevelNum();
            int lines = stats.getLineCount();
            int score = stats.getScore();

            SpriteFont LevelLabel = new SpriteFont("Level " + levels, 320, 300);
            SpriteFont LineslLabel = new SpriteFont("Lines " + lines, 320, 275);
            SpriteFont ScoreLabel = new SpriteFont("Score " + score, 320, 250);

            LevelLabel.Draw();
            LineslLabel.Draw();
            ScoreLabel.Draw();

            SpriteFont Title = new SpriteFont("T E T R I S ", 307, 550);
            SpriteFont End1 = new SpriteFont("  Press the G key", 260, 160);
            SpriteFont End2 = new SpriteFont("   to play again", 260, 140);
            SpriteFont End3 = new SpriteFont("    or press ESC", 260, 100);
            SpriteFont End4 = new SpriteFont("   to exit game!", 260, 80);


            Title.Draw();
            End1.Draw();
            End2.Draw();
            End3.Draw();
            End4.Draw();



        }

        static public void drawStrings(GameStats stats)
        {
            int levels = stats.getLevelNum();
            int lines = stats.getLineCount();
            int score = stats.getScore();

            SpriteFont LevelLabel = new SpriteFont("Level " + levels, 320, 300);
            SpriteFont LineslLabel = new SpriteFont("Lines " + lines, 320, 275);
            SpriteFont ScoreLabel = new SpriteFont("Score " + score, 320, 250);

            LevelLabel.Draw();
            LineslLabel.Draw();
            ScoreLabel.Draw();
        }

        static public void drawInternal(int xPos, int yPos, DrawColor.Shade inColor)
        {
            // This is draw in painted order
            // Draw the color big box first, then the inside..
            Azul.SpriteSolidBox smallBlock = new Azul.SpriteSolidBox(new Azul.Rect(xPos, yPos, Constants.BOX_SIZE - 4, Constants.BOX_SIZE - 4),
                                                                     DrawColor.getColor(inColor));
            smallBlock.Update();

            Azul.SpriteSolidBox bigBlock = new Azul.SpriteSolidBox(new Azul.Rect(xPos, yPos, Constants.BOX_SIZE, Constants.BOX_SIZE),
                                                                   DrawColor.getColor(DrawColor.Shade.COLOR_GREY));
            bigBlock.Update();

            // Draw
            bigBlock.Render();
            smallBlock.Render();
        }

        public static void drawBox(int xPos, int yPos, DrawColor.Shade inColor)
        {
            // This is draw in painted order
            // Draw the color big box first, then the inside.
            int x = (xPos + 1) * Constants.BOX_SIZE + Constants.BOX_SIZE_HALF;
            int y = (yPos + 1) * Constants.BOX_SIZE + Constants.BOX_SIZE_HALF;

            drawInternal(x, y, inColor);
        }

        static public void drawPreviewWindow(int xPos, int yPos, int sizeX, int sizeY, DrawColor.Shade inColor, DrawColor.Shade outColor)
        {
            // This is draw in painted order
            // Draw the color big box first, then the inside..

            Azul.SpriteSolidBox smallBlock = new Azul.SpriteSolidBox(new Azul.Rect(xPos, yPos, sizeX - 4, sizeY - 4),
                                                                     DrawColor.getColor(inColor));
            smallBlock.Update();

            Azul.SpriteSolidBox bigBlock = new Azul.SpriteSolidBox(new Azul.Rect(xPos, yPos, sizeX, sizeY),
                                                                   DrawColor.getColor(DrawColor.Shade.COLOR_GREY));
            bigBlock.Update();

            // draw
            bigBlock.Render();
            smallBlock.Render();
        }

        static public void drawBackground()
        {
            int i;

            // Draw the bottom Bar
            int start_x = Constants.BOX_SIZE_HALF;

            for (i = 0; i < 12; i++)
            {
                drawInternal(start_x + i * Constants.BOX_SIZE, Constants.BOX_SIZE_HALF, DrawColor.Shade.COLOR_DK_GREY);
            }

            // Draw the left and right bar
            start_x = 11 * Constants.BOX_SIZE + Constants.BOX_SIZE_HALF;

            for (i = 0; i < 31; i++)
            {
                drawInternal(start_x, Constants.BOX_SIZE_HALF + i * Constants.BOX_SIZE, DrawColor.Shade.COLOR_DK_GREY);
                drawInternal(Constants.BOX_SIZE_HALF, Constants.BOX_SIZE_HALF + i * Constants.BOX_SIZE, DrawColor.Shade.COLOR_DK_GREY);
            }

            // preview window
            drawPreviewWindow((Constants.PREVIEW_WINDOW_X + 1) * Constants.BOX_SIZE + Constants.BOX_SIZE_HALF,
                               (Constants.PREVIEW_WINDOW_Y + 1) * Constants.BOX_SIZE + Constants.BOX_SIZE_HALF,
                               9 * Constants.BOX_SIZE, 7 * Constants.BOX_SIZE,
                               DrawColor.Shade.COLOR_BACKGROUND_CUSTOM,
                               DrawColor.Shade.COLOR_DK_GREY);
        }
    }
}
